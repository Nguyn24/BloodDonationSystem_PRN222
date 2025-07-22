using BloodDonationSystem.DAL.Repositories.BloodStoredRepo;
using BloodDonationSystem.DAL.Repositories.BloodTypeRepo;
using BloodDonationSystem.DAL.Repositories.Requests;
using BusinessObject.Entities;

namespace BloodDonationSystem.BLL.Services.BloodStoredService;

public class BloodStoredService : IBloodStoredService
{
    private readonly IBloodStoredRepo _bloodStoredRepo;

    public BloodStoredService(IBloodStoredRepo bloodStoredRepo)
    {
        _bloodStoredRepo = bloodStoredRepo;
    }

    public async Task<List<BloodStored>> GetBloodStoredTypes()
    {
        return await _bloodStoredRepo.GetBloodStoredTypes();
    }

    public async Task UpdateBloodStoredType(UpdateBloodStoredRequest request)
    {
        await _bloodStoredRepo.UpdateBloodStoredType(request);
    }
}