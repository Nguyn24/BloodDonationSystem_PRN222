using BloodDonationSystem.DAL.Repositories.Requests;
using BusinessObject.Entities;

namespace BloodDonationSystem.DAL.Repositories.DonationHistoryRepo;

public interface IDonationHistoryRepo
{
    Task<List<DonationsHistory>> GetDonationHistoryAsync();

}