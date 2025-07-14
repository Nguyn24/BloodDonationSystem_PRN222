using BloodDonationSystem.DAL.DBContext;
using BloodDonationSystem.DAL.Repositories.Requests;
using BloodDonationSystem.DAL.Repositories.Responses;
using BloodDonationSystem.DAL.Shared;
using BusinessObject.Entities;
using BusinessObject.Entities.Enum;
using Microsoft.EntityFrameworkCore;
using DonorInformation = BloodDonationSystem.DAL.Repositories.Responses.DonorInformation;

namespace BloodDonationSystem.DAL.Repositories.UserRepo;

public class UserRepo : IUserRepo
{
    private readonly BloodDonationPrn222Context context;
    private readonly UserContext userContext;
    private readonly PasswordHasher passwordHasher;
    
    public async Task<User?> GetByEmailAndPasswordAsync(string email, string password)
    {
        var user = context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        if (user.Role == UserRole.Admin)
        {
            return new User
            {
                Email = email,
                Name = "Admin",
                Role = UserRole.Admin,
            };
        }
        return await context.Users
            .FirstOrDefaultAsync(a => a.Email == email && a.Password == password);
    }
    
    public async Task<List<User>> GetUsersAsync()
    {
        return await context.Users.ToListAsync();
    }

    public async Task<GetCurrentUserResponse> GetCurrentUserAsync()
    {
        var currentUserId = userContext.UserId;

        var user = await context.Users
            .Include(u => u.BloodType)
            .Include(u => u.DonorInformation)
            .FirstOrDefaultAsync(p => p.UserId == currentUserId);
        
        var response = new GetCurrentUserResponse
        {
            FullName = user.Name,
            Email = user.Email,
            Role = user.Role.ToString(),
            BloodTypeName = user.BloodType != null ? user.BloodType.Name : null,
            DateOfBirth = user.DateOfBirth,
            Phone = user.Phone,
            Address = user.Address,
            Gender = user.Gender,
            IsDonor = user.IsDonor,
            LastDonationDate = user.LastDonationDate,
            Status = user.Status,
            DonorInformation = user.DonorInformation == null ? null : new DonorInformation
            {
                Weight = user.DonorInformation.Weight,
                Height = user.DonorInformation.Height,
                MedicalStatus = user.DonorInformation.MedicalStatus.ToString() ?? "Unknown",
                LastChecked = user.DonorInformation.LastChecked
            }
        };

        return response;
    }


    public async Task<List<User>> GetUserByNameAsync(string userName)
    {
        return await context.Users
            .Where(a => a.Name.Contains(userName))
            .ToListAsync();
    }

    public async Task CreateUserAsync(CreateUsersRequest request)
    {
        var hashedPassword = passwordHasher.Hash(request.Password);

        var user = new User
        {
            UserId = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            Password = hashedPassword,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            Address = request.Address,
            Phone = request.Phone,
            Role = request.Role,
            Status = UserStatus.Active,
            IsDonor = false
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();
        
    }
    
    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await context.Users
            .FirstOrDefaultAsync(a => a.UserId == id);
    }

    public async Task UpdateUserAsync(UpdateUserRequest request)
    {
        var user = await context.Users
            .Include(u => u.BloodType)
            .FirstOrDefaultAsync(u => u.UserId == request.UserId);
        
        user.Name = request.FullName ?? user.Name;
        user.Email = request.Email ?? user.Email;
        user.DateOfBirth = request.DateOfBirth ?? user.DateOfBirth;
        user.Gender = request.Gender ?? user.Gender;
        user.Address = request.Address ?? user.Address;
        user.Phone = request.Phone ?? user.Phone;
        
        user.Role = request.Role ?? user.Role;
        user.Status = request.Status ?? user.Status;
        user.IsDonor = request.IsDonor ?? user.IsDonor;
        user.BloodType.Name = request.BloodType ?? user.BloodType.Name;

        await context.SaveChangesAsync();
        
    }

    public async Task DeleteUserAsync(User user)
    {
        var result = await context.Users
            .FirstOrDefaultAsync(a => a.UserId == user.UserId);
        result.Status = UserStatus.Inactive;
        await context.SaveChangesAsync();
    }
    
    public async Task<List<User>> SearchAsync(string keyword) =>
        await context.Users
            .Where(a => a.Name.Contains(keyword) || a.Email.Contains(keyword))
            .ToListAsync();

    public async Task Register(RegisterRequest request)
    {
        var hashedPassword = passwordHasher.Hash(request.Password);

        var user = new User
        {
            UserId = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            Password = hashedPassword,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            Address = request.Address,
            Phone = request.Phone,
            Role = UserRole.Member,
            Status = UserStatus.Active,
            IsDonor = true,
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();
    }

}