using BloodDonationSystem.DAL.DBContext;
using BloodDonationSystem.DAL.Repositories.Requests;
using BusinessObject.DTO;
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
        return await context.DonationsHistories.ToListAsync();
    }
    public async Task<List<DonationsHistory>> GetMyDonationHistoryAsync()
    {
        var userId = userContext.UserId;
        return await context.DonationsHistories
            .Where(h => h.UserId == userId)
            .ToListAsync();
    }

    public async Task<HistoryDateDto> GetDonationHistoryByMonthAsync()
    {
        var result = await context.DonationsHistories
            .GroupBy(h => h.Date.Month)
            .Select(g => new MonthlyDonationDto
            {
                Month = g.Key,
                Count = g.Count()
            })
            .ToListAsync();

        // Đảm bảo đủ 12 tháng (nếu tháng nào không có dữ liệu thì Count = 0)
        var fullYear = Enumerable.Range(1, 12)
            .Select(m => new MonthlyDonationDto
            {
                Month = m,
                Count = result.FirstOrDefault(r => r.Month == m)?.Count ?? 0
            })
            .ToList();

        return new HistoryDateDto
        {
            MonthlyDonation = fullYear,
            TotalCount = fullYear.Sum(m => m.Count)
        };
    }

}