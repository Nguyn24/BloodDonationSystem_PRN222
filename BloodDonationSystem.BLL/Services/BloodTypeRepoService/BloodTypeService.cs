using BloodDonationSystem.DAL.Repositories.BloodTypeRepo;
using BusinessObject.Entities;

namespace BloodDonationSystem.BLL.Services.BloodTypeRepoService;

public class BloodTypeService : IBloodTypeService
{
    private readonly IBloodTypeRepo _bloodTypeRepo;

    public async Task<List<BloodType>> GetBloodTypeAsync()
    {
        return await _bloodTypeRepo.GetBloodTypeAsync();
    }
}