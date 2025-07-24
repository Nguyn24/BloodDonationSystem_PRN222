using BloodDonationSystem.DAL.Repositories.BloodTypeRepo;
using BusinessObject.Entities;

namespace BloodDonationSystem.BLL.Services.BloodTypeRepoService;

public class BloodTypeService : IBloodTypeService
{
    private readonly IBloodTypeRepo _bloodTypeRepo;
    public BloodTypeService(IBloodTypeRepo bloodTypeRepo)
    {
        _bloodTypeRepo = bloodTypeRepo;
    }
    
    public async Task<List<BloodType>> GetBloodTypeAsync()
    {
        return await _bloodTypeRepo.GetBloodTypeAsync();
    }
    public async Task<BloodType> GetBloodTypeByIDAsync(Guid id)
    {
        return await _bloodTypeRepo.GetBloodTypeByIdAsync(id) 
               ?? throw new KeyNotFoundException("Blood type not found");
    }
}