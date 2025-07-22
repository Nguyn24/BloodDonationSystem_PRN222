
using BloodDonationSystem.DAL.Repositories.DonationHistoryRepo;
using BusinessObject.Entities;

namespace BloodDonationSystem.BLL.Services.DonationHistoryService;

public class DonationHistoryService : IDonationHistoryService
{
    private readonly IDonationHistoryRepo _donationHistoryRepo;

    public async Task<List<DonationsHistory>> GetDonationHistoryAsync()
    {
        return await _donationHistoryRepo.GetDonationHistoryAsync();
    }
    public async Task<List<DonationsHistory>> GetMyDonationHistoryAsync()
    {
        return await _donationHistoryRepo.GetMyDonationHistoryAsync();
    }
}