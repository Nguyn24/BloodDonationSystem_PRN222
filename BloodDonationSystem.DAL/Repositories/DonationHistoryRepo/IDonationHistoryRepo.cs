using BloodDonationSystem.DAL.Repositories.Requests;
using BusinessObject.DTO;
using BusinessObject.Entities;

namespace BloodDonationSystem.DAL.Repositories.DonationHistoryRepo;

public interface IDonationHistoryRepo
{
    Task<List<DonationsHistory>> GetDonationHistoryAsync();
    Task<List<DonationsHistory>> GetMyDonationHistoryAsync();
    Task<HistoryDateDto> GetDonationHistoryByMonthAsync();

}