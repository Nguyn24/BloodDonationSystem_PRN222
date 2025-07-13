namespace BusinessObject.Entities;

public partial class BloodStored
{
    public Guid StoredId { get; set; }

    public Guid BloodTypeId { get; set; }

    public int Quantity { get; set; }

    public DateTimeOffset LastUpdated { get; set; }

    public virtual BloodType BloodType { get; set; } = null!;
}
