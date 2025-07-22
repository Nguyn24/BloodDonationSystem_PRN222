using BloodDonationSystem.DAL.DBContext;
using BloodDonationSystem.DAL.Repositories.Requests;
using BusinessObject.Entities;
using BusinessObject.Entities.Enum;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;


namespace BloodDonationSystem.DAL.Repositories.DonationRequestRepo;

public class DonationRequestRepo : IDonationRequestRepo
{
    private readonly BloodDonationPrn222Context context;
    private readonly UserContext userContext;

    public async Task CreateDonationRequestAsync(CreateDonationRequest request)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.UserId == userContext.UserId);
        var bloodType = await context.BloodTypes.FirstOrDefaultAsync(b => b.Name == user.BloodType.Name);

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
        return await context.DonationRequests.ToListAsync();
    }

    public async Task<DonationRequest> GetDonationRequestByIdAsync(Guid requestId)
    {
        return await context.DonationRequests
            .FirstOrDefaultAsync(r => r.RequestId == requestId);
    }
    
    public async Task<List<DonationRequest>> GetMyDonationRequestsAsync()
    {
        var userId = userContext.UserId;
        return await context.DonationRequests
            .Where(r => r.UserId == userId)
            .ToListAsync();
    }


    public async Task ConfirmDonationRequestAsync(Guid requestId)
    {
         var donationRequest = await context.DonationRequests
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.RequestId == requestId);
         
        if (donationRequest.User?.IsDonor == true)
        {
            donationRequest.Status = DonationRequestStatus.Scheduled;
        }
        await context.SaveChangesAsync();
    }

    public async Task CompleteDonationRequestAsync(Guid requestId, int amountBlood)
    {
        var donationRequest = await context.DonationRequests
            .FirstOrDefaultAsync(r => r.RequestId == requestId);
        
        var bloodStored = await context.BloodStoreds
            .FirstOrDefaultAsync(b => b.BloodTypeId == donationRequest.BloodTypeId);
        
        donationRequest.AmountBlood = amountBlood;

        bloodStored.Quantity += donationRequest.AmountBlood;
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

        donationRequest.Status = DonationRequestStatus.Completed;

        await context.SaveChangesAsync();
    }

    public async Task UpdateFailedDonationRequestAsync(Guid requestId, string reason)
    {
        var donationRequest = await context.DonationRequests
            .FirstOrDefaultAsync(r => r.RequestId == requestId);

        donationRequest.Status = DonationRequestStatus.Failed;
        donationRequest.Note = reason;
        await context.SaveChangesAsync();
    }
}