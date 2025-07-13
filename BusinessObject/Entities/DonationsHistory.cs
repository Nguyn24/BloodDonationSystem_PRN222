using BusinessObject.Entities.Enum;

namespace BusinessObject.Entities;

public partial class DonationsHistory
{
    public Guid DonationId { get; set; }

    public Guid UserId { get; set; }

    public Guid RequestId { get; set; }

    public DateTimeOffset Date { get; set; }

    public DonationHistoryStatus Status { get; set; } 

    public Guid ConfirmedBy { get; set; }

    public virtual User ConfirmedByNavigation { get; set; } = null!;

    public virtual DonationRequest Request { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
