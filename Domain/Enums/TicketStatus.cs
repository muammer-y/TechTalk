using System.ComponentModel;

namespace Domain.Enums;

public enum TicketStatus
{
    [Description("Bekliyor")]
    Pending = 10, // for ticket default status it can be assigned as 0

    [Description("Onaylandı")]
    Approved = 20,

    [Description("Tamamlandı")]
    Completed = 30
}