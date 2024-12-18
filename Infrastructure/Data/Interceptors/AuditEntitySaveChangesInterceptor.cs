using Domain.Entities.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;

namespace Infrastructure.Data.Interceptors;

//This interceptor can also be seperated for AuditEntity and SoftDeletion
//Another use case can be publishing events from the interceptor.
//When a entity has relative update or create event those events can be published from a central base

/// <summary>
/// Handles AuditEntity updates before commiting to db
/// </summary>
public class AuditEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditEntitySaveChangesInterceptor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var entries = eventData.Context?.ChangeTracker?.Entries() ?? [];

        string currentUser = GetCurrentUser();

        ProcessAuditEntities(entries, currentUser);
        ProcessSoftDeleteEntities(entries, currentUser);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void ProcessAuditEntities(IEnumerable<EntityEntry> entries, string currentUser)
    {
        var auditEntries = entries.Where(entry => entry.Entity is BaseAuditableEntity);

        foreach (var entry in auditEntries)
        {
            if (entry.State == EntityState.Added)
            {
                SetCreatedFields((BaseAuditableEntity)entry.Entity, currentUser);
            }
            else if (entry.State == EntityState.Modified)
            {
                SetUpdatedFields((BaseAuditableEntity)entry.Entity, currentUser);
            }
        }
    }

    private static void ProcessSoftDeleteEntities(IEnumerable<EntityEntry> entries, string currentUser)
    {
        var softDeleteEntries = entries.Where(entry => entry.Entity is ISoftDelete && entry.State == EntityState.Deleted);

        foreach (var entry in softDeleteEntries)
        {
            if (entry.Entity is ISoftDelete softDeletableEntity)
            {
                softDeletableEntity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }

            if (entry.Entity is BaseAuditableEntity auditableEntity)
            {
                SetUpdatedFields(auditableEntity, currentUser);
            }
        }
    }

    private string GetCurrentUser()
    {
        var email = _httpContextAccessor.HttpContext?.User?.GetUserEmail();
        return string.IsNullOrEmpty(email) ? "System" : email;
    }

    private static void SetCreatedFields(BaseAuditableEntity entity, string currentUser)
    {
        entity.CreatedDate = DateTime.UtcNow;
        entity.CreatedBy = currentUser;
    }

    private static void SetUpdatedFields(BaseAuditableEntity entity, string currentUser)
    {
        entity.LastModifiedDate = DateTime.UtcNow;
        entity.LastModifiedBy = currentUser;
    }
}