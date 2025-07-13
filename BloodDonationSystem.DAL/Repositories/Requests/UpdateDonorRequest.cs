namespace BloodDonationSystem.DAL.Repositories.Requests;

public class UpdateDonorRequest
{
    public Guid DonorInfoId { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Height { get; set; }
    public string? MedicalStatus { get; set; }
    public DateTime LastChecked { get; set; }
}