using BloodDonationSystem.DAL.DBContext;
using BusinessObject.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSystem.DAL.Repositories.BloodTypeRepo;

public class BloodTypeRepo : IBloodTypeRepo
{
    private readonly BloodDonationPrn222Context context;
    
    public async Task<List<BloodType>> GetBloodTypeAsync()
    {
        return await context.BloodTypes.ToListAsync();
    }

}