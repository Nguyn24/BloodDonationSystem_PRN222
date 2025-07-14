using BusinessObject.Entities;

namespace BloodDonationSystem.BLL.Services.DonationHistoryService;

public interface IDonationHistoryService
{
    Task<List<DonationsHistory>> GetDonationHistoryAsync();

}