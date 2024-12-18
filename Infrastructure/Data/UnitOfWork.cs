using Domain.Entities;
using Domain.Entities.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace Infrastructure.Data;

public interface IUnitOfWork : IDisposable
{
    public IRepository<User> UserRepository { get; }
    public IRepository<Ticket> TicketRepository { get; }

    Task<int> SaveChangesAsync();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IServiceProvider _serviceProvider;
    private bool _disposed = false;

    private readonly ConcurrentDictionary<Type, object> _repositories = new();

    public UnitOfWork(AppDbContext context, IServiceProvider serviceProvider)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public IRepository<User> UserRepository => GetRepository<User>();

    public IRepository<Ticket> TicketRepository => GetRepository<Ticket>();

    private IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IBaseEntity
    {
        var type = typeof(TEntity);

        if (!_repositories.TryGetValue(type, out var repository))
        {
            repository = _serviceProvider.GetRequiredService<IRepository<TEntity>>();
            _repositories[type] = repository;
        }

        return (IRepository<TEntity>)repository;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }
    }
}
