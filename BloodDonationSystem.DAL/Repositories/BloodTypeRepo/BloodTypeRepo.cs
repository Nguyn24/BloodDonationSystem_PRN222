using BloodDonationSystem.DAL.DBContext;
using BusinessObject.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSystem.DAL.Repositories.BloodTypeRepo;

public class BloodTypeRepo : IBloodTypeRepo
{
    private readonly BloodDonationPrn222Context context;

    public BloodTypeRepo(BloodDonationPrn222Context _context)
    {
        context = _context;
    }
    public async Task<List<BloodType>> GetBloodTypeAsync()
    {
        return await context.BloodTypes.ToListAsync();
    }

    public async Task UpdateBloodTypeAsync(BloodType bloodType)
    {
        context.BloodTypes.Update(bloodType);
        await context.SaveChangesAsync();
    }

    public async Task CreateBloodTypeAsync(BloodType bloodType)
    {
        await context.BloodTypes.AddAsync(bloodType);
        await context.SaveChangesAsync();
    }

    public Task DeleteBloodTypeAsync(BloodType bloodType)
    {
        context.BloodTypes.Remove(bloodType);
        return context.SaveChangesAsync();
    }
}