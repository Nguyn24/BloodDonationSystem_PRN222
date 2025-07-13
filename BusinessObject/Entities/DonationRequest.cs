using BusinessObject.Entities.Enum;

namespace BusinessObject.Entities;

public partial class DonationRequest
{
    public Guid RequestId { get; set; }

    public Guid BloodTypeId { get; set; }

    public Guid UserId { get; set; }

    public int AmountBlood { get; set; }

    public DateTimeOffset RequestTime { get; set; }

    public string? ContactName { get; set; }

    public string? ContactPhone { get; set; }

    public DonationRequestStatus Status { get; set; } 

    public string? Note { get; set; }

    public virtual BloodType BloodType { get; set; } = null!;

    public virtual ICollection<DonationsHistory> DonationsHistories { get; set; } = new List<DonationsHistory>();

    public virtual User User { get; set; } = null!;
}
