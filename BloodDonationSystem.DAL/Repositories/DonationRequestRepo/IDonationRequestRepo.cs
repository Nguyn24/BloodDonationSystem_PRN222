using BloodDonationSystem.DAL.Repositories.Requests;
using BusinessObject.Entities;
using BusinessObject.Entities.Enum;

namespace BloodDonationSystem.DAL.Repositories.DonationRequestRepo;

public interface IDonationRequestRepo
{
    Task CreateDonationRequestAsync(CreateDonationRequest request);
    Task<List<DonationRequest>> GetDonationRequestAsync();
    Task<DonationRequest> GetDonationRequestByIdAsync(Guid requestId);
    Task<List<DonationRequest>> GetMyDonationRequestsAsync();
    Task DeleteDonationRequestAsync(Guid requestId);
    Task ConfirmDonationRequestAsync(Guid requestId);
    Task CompleteDonationRequestAsync(Guid requestId, int amountBlood);
    Task UpdateFailedDonationRequestAsync(Guid requestId, string reason);
    Task<List<DonationRequest>> GetRequestsByStatusAsync(DonationRequestStatus status);

}