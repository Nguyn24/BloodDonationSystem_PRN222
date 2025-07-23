using BloodDonationSystem.DAL.Repositories.Requests;
using BusinessObject.Entities;

namespace BloodDonationSystem.DAL.Repositories.DonorInformationRepo;

public interface IDonorInformationRepo 
{
    Task CreateDonorAsync(CreateDonorRequest request);
    Task<List<DonorInformation>> GetDonorInfoAsync();

    Task UpdateDonorAsync(UpdateDonorRequest request);

    Task DeleteDonorAsync(DonorInformation donor);

    Task<DonorInformation> GetById(Guid id);
}