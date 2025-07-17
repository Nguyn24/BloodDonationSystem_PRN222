using BusinessObject.Entities;

namespace BloodDonationSystem.DAL.Repositories.BloodTypeRepo;

public interface IBloodTypeRepo
{
    Task<List<BloodType>> GetBloodTypeAsync();
    Task UpdateBloodTypeAsync(BloodType bloodType);
    Task CreateBloodTypeAsync(BloodType bloodType);
    Task DeleteBloodTypeAsync(BloodType bloodType);
}