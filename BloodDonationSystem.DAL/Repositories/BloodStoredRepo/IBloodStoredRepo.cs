using BloodDonationSystem.DAL.Repositories.Requests;
using BusinessObject.Entities;

namespace BloodDonationSystem.DAL.Repositories.BloodStoredRepo;

public interface IBloodStoredRepo
{
    public Task<List<BloodStored>> GetBloodStoredTypes();
    public Task UpdateBloodStoredType(UpdateBloodStoredRequest request);
}