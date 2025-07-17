using BloodDonationSystem.DAL.Repositories.Requests;
using BusinessObject.Entities;

namespace BloodDonationSystem.BLL.Services.DonationRequestService;

public interface IDonationRequestService
{
    Task CreateDonationRequestAsync(CreateDonationRequest request);
    Task<List<DonationRequest>> GetDonationRequestAsync();
    Task DeleteDonationRequestAsync(Guid requestId);
    Task ConfirmDonationRequestAsync(Guid requestId);
    Task CompleteDonationRequestAsync(Guid requestId, int amountBlood);
    Task UpdateFailedDonationRequestAsync(Guid requestId, string reason);
}