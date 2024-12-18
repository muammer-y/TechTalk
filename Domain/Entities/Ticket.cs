using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Ticket : BaseAuditableEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public TicketStatus Status { get; set; }
}
