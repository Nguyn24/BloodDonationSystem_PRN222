namespace BloodDonationSystem.DAL.Repositories.Requests;

public class CreateDonationRequest
{
    public int AmountBlood { get; set; }
    public DateTime Date { get; set; }
    public string? Phone { get; set; }
    public string? Note { get; set; }
}