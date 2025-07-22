using BloodDonationSystem.DAL.Repositories.Requests;
using BloodDonationSystem.DAL.Repositories.Responses;
using BloodDonationSystem.DAL.Repositories.UserRepo;
using BusinessObject.Entities;
using BusinessObject.Entities.Enum;
using Microsoft.Extensions.Configuration;

namespace BloodDonationSystem.BLL.Services.UserService;

public class UserService : IUserService
{
    private readonly IUserRepo _userRepo;

    public UserService(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }

    public Task<List<User>> GetUsersAsync()
        => _userRepo.GetUsersAsync();

    public Task<User?> GetByEmailAndPasswordAsync(string email, string password)
        => _userRepo.GetByEmailAndPasswordAsync(email, password);

    public Task<List<User>> SearchAsync(string keyword)
    {
        return _userRepo.SearchAsync(keyword);
    }

    public Task Register(RegisterRequest request)
    {
        return _userRepo.Register(request);
    }

    public Task<GetCurrentUserResponse> GetCurrentUserAsync() => _userRepo.GetCurrentUserAsync();

    public async Task<User?> AuthenticateAsync(string email, string password)
    {
        return await _userRepo.GetByEmailAndPasswordAsync(email, password);
    }

    public async Task CreateUserAsync(CreateUsersRequest request)
    {
        await _userRepo.CreateUserAsync(request);
    }

    public async Task UpdateUserAsync(UpdateUserRequest request)
    {
        await _userRepo.UpdateUserAsync(request);
    }

    public async Task DeleteUserAsync(User user)
    {
        await _userRepo.DeleteUserAsync(user);
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _userRepo.GetUserByIdAsync(id);
    }

    public async Task<List<User>> GetUserByNameAsync(string userName)
    {
        return await _userRepo.GetUserByNameAsync(userName);
    }
}