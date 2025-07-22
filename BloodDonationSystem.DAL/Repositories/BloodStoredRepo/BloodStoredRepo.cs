using BloodDonationSystem.DAL.DBContext;
using BloodDonationSystem.DAL.Repositories.Requests;
using BusinessObject.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSystem.DAL.Repositories.BloodStoredRepo;

public class BloodStoredRepo : IBloodStoredRepo
{
    private readonly BloodDonationPrn222Context context;

    public BloodStoredRepo(BloodDonationPrn222Context context)
    {
        this.context = context;
    }

    public async Task<List<BloodStored>> GetBloodStoredTypes()
    {
        return await context.BloodStoreds.ToListAsync();
    }

    public async Task UpdateBloodStoredType(UpdateBloodStoredRequest request)
    {
        var bloodType = await context.BloodTypes
            .FirstOrDefaultAsync(b => b.Name == request.BloodTypeName);
        var stored = await context.BloodStoreds
            .FirstOrDefaultAsync(s => s.BloodTypeId == bloodType.BloodTypeId);
        
        if (request.Quantity.HasValue)
        {
            var newQuantity = stored.Quantity + request.Quantity.Value;
            // if (newQuantity < 0)
            // {
            //     return Result.Failure<UpdateBloodStoredResponse>(BloodErrors.QuantityInvalid);
            // }

            stored.Quantity = newQuantity;
            stored.LastUpdated = DateTime.UtcNow;
        }
        await context.SaveChangesAsync();
    }
}