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
    public UserRepo(BloodDonationPrn222Context context, UserContext userContext, PasswordHasher passwordHasher)
    {
        this.context = context;
        this.userContext = userContext;
        this.passwordHasher = passwordHasher;
    }

    public async Task<User?> GetByEmailAndPasswordAsync(string email, string password)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
            return null;

        // ✅ Check password trước
        if (!passwordHasher.Verify(password, user.Password))
            return null;

        // ✅ Nếu là Admin, có thể xử lý khác nếu cần
        if (user.Role == UserRole.Admin)
        {
            return new User
            {
                Email = user.Email,
                Name = "Admin",
                Role = UserRole.Admin,
            };
        }

        return user;
    }   


    
    public async Task<List<User>> GetUsersAsync()
    {
        return await context.Users
            .Select(u => new User { UserId = u.UserId, Name = u.Name })
            .ToListAsync();
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

    public async Task UpdateUserAsync(User user)
    {
        var existingUser = await context.Users.FindAsync(user.UserId);
        if (existingUser == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        // Cập nhật từng thuộc tính
        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
        existingUser.DateOfBirth = user.DateOfBirth;
        existingUser.BloodTypeId = user.BloodTypeId;
        existingUser.Gender = user.Gender;
        existingUser.Address = user.Address;
        existingUser.Phone = user.Phone;
        existingUser.Role = user.Role;
        existingUser.Status = user.Status;
        existingUser.IsDonor = user.IsDonor;

        context.Users.Update(existingUser); // không bắt buộc nếu entity đã được tracked
        await context.SaveChangesAsync();
    }


    public async Task DeleteUserAsync(Guid userID)
    {
        var result = await context.Users
            .FirstOrDefaultAsync(a => a.UserId == userID);
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
        await context.SaveChangesAsync(); // <- Async OK
    }

    public async Task<List<User>> GetUsersAsyncHavePagination()
    {
        return await context.Users
        .Include(u => u.BloodType)
        .Where(u => u.Status == UserStatus.Active)
        .ToListAsync();
    }
}