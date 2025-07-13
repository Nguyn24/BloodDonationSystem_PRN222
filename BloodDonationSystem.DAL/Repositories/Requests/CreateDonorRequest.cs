namespace BloodDonationSystem.DAL.Repositories.Requests;

public class CreateDonorRequest
{
    public Guid UserId { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public string? MedicalStatus { get; set; }
    public DateTime LastChecked { get; set; }
}