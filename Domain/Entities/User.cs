using Domain.Entities.Common;

namespace Domain.Entities;

public class User : BaseAuditableEntity, ISoftDelete
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }

    public bool IsDeleted { get; set; }
};
