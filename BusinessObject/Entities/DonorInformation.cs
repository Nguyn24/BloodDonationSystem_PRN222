namespace BusinessObject.Entities;

public partial class DonorInformation
{
    public Guid DonorInfoId { get; set; }

    public Guid UserId { get; set; }

    public decimal Weight { get; set; }

    public decimal Height { get; set; }

    public string? MedicalStatus { get; set; } 

    public DateTimeOffset LastChecked { get; set; }

    public virtual User User { get; set; } = null!;
}
