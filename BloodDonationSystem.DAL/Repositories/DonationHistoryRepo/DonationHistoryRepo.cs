using BloodDonationSystem.DAL.DBContext;
using BloodDonationSystem.DAL.Repositories.Requests;
using BusinessObject.Entities;
using BusinessObject.Entities.Enum;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSystem.DAL.Repositories.DonationHistoryRepo;

public class DonationHistoryRepo : IDonationHistoryRepo
{
    private readonly BloodDonationPrn222Context context;

    public async Task<List<DonationsHistory>> GetDonationHistoryAsync()
    {
        return await context.DonationsHistories.ToListAsync();
    }
}