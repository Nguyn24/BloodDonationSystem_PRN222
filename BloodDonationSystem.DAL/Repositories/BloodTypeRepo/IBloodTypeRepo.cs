using BusinessObject.Entities;

namespace BloodDonationSystem.DAL.Repositories.BloodTypeRepo;

public interface IBloodTypeRepo
{
    Task<List<BloodType>> GetBloodTypeAsync();

}