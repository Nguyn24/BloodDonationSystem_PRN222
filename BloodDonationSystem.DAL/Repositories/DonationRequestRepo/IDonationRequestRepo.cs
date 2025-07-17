using BloodDonationSystem.DAL.Repositories.Requests;
using BusinessObject.Entities;

namespace BloodDonationSystem.DAL.Repositories.DonationRequestRepo;

public interface IDonationRequestRepo
{
    Task CreateDonationRequestAsync(CreateDonationRequest request);
    Task<List<DonationRequest>> GetDonationRequestAsync();
    Task DeleteDonationRequestAsync(Guid requestId);
    Task ConfirmDonationRequestAsync(Guid requestId);
    Task CompleteDonationRequestAsync(Guid requestId, int amountBlood);
    Task UpdateFailedDonationRequestAsync(Guid requestId, string reason);
}