namespace Domain.Entities.Common;

public interface IBaseEntity
{
    public int Id { get; set; } // this also can be generic type for strongly typed Ids. Sample is below
}

public interface IBaseEntityGeneric<T>
{
    /// <summary>
    /// For guid usage either a 3rd party id generator needs to be boxed for ef core or we can use Guid.CreateVersion7(); for sequential ids
    /// </summary>
    T Id { get; set; } 
}

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }
}

public interface IAuditableEntity
{
    public DateTimeOffset CreatedDate { get; set; }
    public string? CreatedBy { get; set; }

    public DateTimeOffset? LastModifiedDate { get; set; }
    public string? LastModifiedBy { get; set; }
}

public abstract class BaseEntity : IBaseEntity
{
    public int Id { get; set; }
}

public abstract class BaseAuditableEntity : IBaseEntity, IAuditableEntity
{
    public int Id { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public string? CreatedBy { get; set; }

    public DateTimeOffset? LastModifiedDate { get; set; }
    public string? LastModifiedBy { get; set; }
}
