using BloodDonationSystem.DAL.DBContext;
using BloodDonationSystem.DAL.Repositories.Requests;
using BusinessObject.Entities;
using BusinessObject.Entities.Enum;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSystem.DAL.Repositories.DonationHistoryRepo;

public class DonationHistoryRepo : IDonationHistoryRepo
{
    private readonly BloodDonationPrn222Context context;
    private readonly UserContext userContext;

    public DonationHistoryRepo(BloodDonationPrn222Context context, UserContext userContext)
    {
        this.context = context;
        this.userContext = userContext;
    }

    public async Task<List<DonationsHistory>> GetDonationHistoryAsync()
    {
        return await context.DonationsHistories
            .Include(dh => dh.User)
            .Include(dh => dh.ConfirmedByNavigation)
            .ToListAsync();
    }

    public async Task<List<DonationsHistory>> GetMyDonationHistoryAsync()
    {
        var userId = userContext.UserId;
        return await context.DonationsHistories
            .Where(h => h.UserId == userId)
            .ToListAsync();
    }
}