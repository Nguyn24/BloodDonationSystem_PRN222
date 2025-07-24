using BloodDonationSystem.DAL.DBContext;
using BloodDonationSystem.DAL.Repositories.Requests;
using BusinessObject.DTO;
using BusinessObject.Entities;
using BusinessObject.Entities.Enum;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;


namespace BloodDonationSystem.DAL.Repositories.DonationRequestRepo;

public class DonationRequestRepo : IDonationRequestRepo
{
    private readonly BloodDonationPrn222Context context;
    private readonly UserContext userContext;

    public DonationRequestRepo(
        BloodDonationPrn222Context dbContext, 
        UserContext userCtx)
    {
        context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        userContext = userCtx ?? throw new ArgumentNullException(nameof(userCtx));
    }

    public async Task<DonationRequest> CreateDonationRequestAsync(CreateDonationRequest request)
    {
        if (context == null)
            throw new Exception("Database context is not initialized.");

        if (userContext == null)
            throw new Exception("User context is not initialized.");

        if (userContext.UserId == null)
            throw new Exception("Cannot fetch user information. UserId is null.");

        var user = await context.Users
            .Include(u => u.BloodType)
            .FirstOrDefaultAsync(u => u.UserId == userContext.UserId);

        if (user == null)
            throw new Exception($"User with ID '{userContext.UserId}' not found.");

        if (user.BloodType == null)
            throw new Exception($"User '{user.UserId}' does not have a BloodType associated.");

        var bloodType = await context.BloodTypes.FirstOrDefaultAsync(b => b.Name == user.BloodType.Name);

        if (bloodType == null)
            throw new Exception($"BloodType '{user.BloodType.Name}' not found in the database.");

        var donationRequest = new DonationRequest
        {
            RequestId = Guid.NewGuid(),
            UserId = user.UserId,
            BloodTypeId = bloodType.BloodTypeId,
            AmountBlood = request.AmountBlood,
            RequestTime = request.Date,
            ContactPhone = request.Phone,
            Note = request.Note,
            Status = DonationRequestStatus.Pending
        };

        context.DonationRequests.Add(donationRequest);
        await context.SaveChangesAsync();
        return donationRequest;
    }

    public async Task DeleteDonationRequestAsync(Guid requestId)
    {
        var donationRequest = await context.DonationRequests
            .FirstOrDefaultAsync(r => r.RequestId == requestId);

        donationRequest.Status = DonationRequestStatus.Cancelled;
        await context.SaveChangesAsync();
    }

    public async Task<List<DonationRequest>> GetDonationRequestAsync()
    {
        return await context.DonationRequests
            .Include(r => r.User)
            .Include(r => r.BloodType)
            .OrderByDescending(r => r.RequestTime)
            .ToListAsync();
    }

    public async Task<DonationRequest> GetDonationRequestByIdAsync(Guid requestId)
    {
        return await context.DonationRequests
            .Include(r => r.User)
            .Include(r => r.BloodType)
            .FirstOrDefaultAsync(r => r.RequestId == requestId);
    }
    
    public async Task<List<DonationRequest>> GetMyDonationRequestsAsync()
    {
        var userId = userContext.UserId;
        return await context.DonationRequests
            .Where(r => r.UserId == userId)
            .ToListAsync();
    }


    public async Task<DonationRequest> ConfirmDonationRequestAsync(Guid requestId)
    {
         var donationRequest = await context.DonationRequests
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.RequestId == requestId);
         
        if (donationRequest.User?.IsDonor == true)
        {
            donationRequest.Status = DonationRequestStatus.Scheduled;
        }
        await context.SaveChangesAsync();
        return donationRequest;
    }

    public async Task<DonationRequest> CompleteDonationRequestAsync(Guid requestId, int amountBlood)
    {
        var donationRequest = await context.DonationRequests
            .FirstOrDefaultAsync(r => r.RequestId == requestId);

        if (donationRequest == null)
            throw new Exception($"DonationRequest with ID {requestId} not found.");

        var bloodStored = await context.BloodStoreds
            .FirstOrDefaultAsync(b => b.BloodTypeId == donationRequest.BloodTypeId);

        if (bloodStored == null)
            throw new Exception($"No BloodStored record found for BloodTypeId {donationRequest.BloodTypeId}");

        // Cập nhật thông tin
        donationRequest.AmountBlood = amountBlood;
        donationRequest.Status = DonationRequestStatus.Completed;

        bloodStored.Quantity += amountBlood;
        bloodStored.LastUpdated = DateTime.UtcNow;

        context.DonationsHistories.Add(new DonationsHistory
        {
            DonationId = Guid.NewGuid(),
            UserId = donationRequest.UserId,
            RequestId = donationRequest.RequestId,
            Date = DateTime.UtcNow,
            Status = DonationHistoryStatus.Completed,
            ConfirmedBy = userContext.UserId
        });

        await context.SaveChangesAsync();
        return donationRequest;
    }

    public async Task<DonationRequest> UpdateFailedDonationRequestAsync(Guid requestId, string reason)
    {
        var donationRequest = await context.DonationRequests
            .FirstOrDefaultAsync(r => r.RequestId == requestId);

        donationRequest.Status = DonationRequestStatus.Failed;
        donationRequest.Note = reason;
        await context.SaveChangesAsync();
        return donationRequest;
    }
    public async Task<List<DonationRequest>> GetRequestsByStatusAsync(DonationRequestStatus status)
    {
        return await context.DonationRequests
            .Include(r => r.User)
            .Include(r => r.BloodType)
            .Where(r => r.Status == status)
            .OrderByDescending(r => r.RequestTime)
            .ToListAsync();
    }

    public async Task<RequestStatusDto> GetDonationRequestsByStatusAsync()
    {
        // Lấy các trạng thái có trong DB
        var groupedStats = await context.DonationRequests
            .GroupBy(r => r.Status)
            .Select(g => new
            {
                Status = g.Key,
                Count = g.Count()
            })
            .ToListAsync();

        // Lấy toàn bộ Enum
        var allStatuses = Enum.GetValues(typeof(DonationRequestStatus))
            .Cast<DonationRequestStatus>();

        // Map về danh sách đầy đủ
        var result = allStatuses
            .Select(statusEnum => new StatusCountDto
            {
                Status = statusEnum.ToString(),
                Count = groupedStats.FirstOrDefault(g => g.Status == statusEnum)?.Count ?? 0
            })
            .ToList();

        var total = await context.DonationRequests.CountAsync();

        return new RequestStatusDto
        {
            Group = result,
            Count = total
        };
    }
}