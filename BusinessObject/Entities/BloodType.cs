namespace BusinessObject.Entities;

public partial class BloodType
{
    public Guid BloodTypeId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<BloodStored> BloodStoreds { get; set; } = new List<BloodStored>();

    public virtual ICollection<DonationRequest> DonationRequests { get; set; } = new List<DonationRequest>();
}
