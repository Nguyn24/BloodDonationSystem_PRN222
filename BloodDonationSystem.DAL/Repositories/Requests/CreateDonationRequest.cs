namespace BloodDonationSystem.DAL.Repositories.Requests;

public class CreateDonationRequest
{
    public int AmountBlood { get; set; }
    public string? Note { get; set; }
}