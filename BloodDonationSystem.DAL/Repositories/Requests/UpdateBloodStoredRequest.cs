namespace BloodDonationSystem.DAL.Repositories.Requests;

public class UpdateBloodStoredRequest
{
    public Guid StoredId { get; set; }
    public string BloodTypeName { get; set; } = default!;
    public int? Quantity { get; set; }
}