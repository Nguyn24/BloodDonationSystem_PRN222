using BloodDonationSystem.DAL.DBContext;
using BloodDonationSystem.DAL.Repositories.Requests;
using BusinessObject.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSystem.DAL.Repositories.DonorInformationRepo;

public class DonorInformationRepo : IDonorInformationRepo
{
    private readonly BloodDonationPrn222Context context;
    private readonly UserContext userContext;

    public DonorInformationRepo(BloodDonationPrn222Context _context)
    {
        context = _context;
        //userContext = _userContext;
    }
    public async Task<List<DonorInformation>> GetDonorInfoAsync()
    {
        return await context.DonorInformations.ToListAsync();
    }

    public async Task CreateDonorAsync(CreateDonorRequest request)
    {
        var userId = userContext.UserId;
        var user = await context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
        
        var donorInfo = new DonorInformation
        {
            UserId = user.UserId,
            DonorInfoId = Guid.NewGuid(),
            Weight = request.Weight,
            Height = request.Height,
            MedicalStatus = request.MedicalStatus,
            LastChecked = DateTime.UtcNow
        };

        await context.DonorInformations.AddAsync(donorInfo);
        await context.SaveChangesAsync();
    }

    public async Task UpdateDonorAsync(UpdateDonorRequest request)
    {
        var donorInfo = await context.DonorInformations
            .Include(d => d.User)
            .FirstOrDefaultAsync(d => d.DonorInfoId == request.DonorInfoId);
        
        donorInfo.Weight = request.Weight ?? donorInfo.Weight;
        donorInfo.Height = request.Height ?? donorInfo.Height;
        donorInfo.MedicalStatus = request.MedicalStatus;
        donorInfo.LastChecked = DateTime.UtcNow;

        await context.SaveChangesAsync();
    }

    public async Task DeleteDonorAsync(DonorInformation donor)
    {
         context.DonorInformations.Remove(donor);
         await context.SaveChangesAsync();
    }

    public async Task<DonorInformation> GetById(Guid id)
    {
        return await context.DonorInformations
            .Include(d => d.User)
            .FirstOrDefaultAsync(d => d.DonorInfoId == id);
    }
}