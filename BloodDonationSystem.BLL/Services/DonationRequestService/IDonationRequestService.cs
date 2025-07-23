using BloodDonationSystem.DAL.Repositories.Requests;
using BusinessObject.Entities;

namespace BloodDonationSystem.BLL.Services.DonationRequestService;

public interface IDonationRequestService
{
    Task CreateDonationRequestAsync(CreateDonationRequest request);
    Task<List<DonationRequest>> GetDonationRequestAsync();
    Task<DonationRequest> GetDonationRequestByIdAsync(Guid requestId);
    Task<List<DonationRequest>> GetMyDonationRequestsAsync( );
    Task DeleteDonationRequestAsync(Guid requestId);
    Task<DonationRequest> ConfirmDonationRequestAsync(Guid requestId);
    Task<DonationRequest> CompleteDonationRequestAsync(Guid requestId, int amountBlood);
    Task<DonationRequest> UpdateFailedDonationRequestAsync(Guid requestId, string reason);
}