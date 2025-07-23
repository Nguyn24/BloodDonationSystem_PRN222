using BusinessObject.DTO;
using BusinessObject.Entities;

namespace BloodDonationSystem.BLL.Services.DonationHistoryService;

public interface IDonationHistoryService
{
    Task<List<DonationsHistory>> GetDonationHistoryAsync();
    Task<List<DonationsHistory>> GetMyDonationHistoryAsync();
    Task<HistoryDateDto> GetDonationHistoryByMonthAsync();
}