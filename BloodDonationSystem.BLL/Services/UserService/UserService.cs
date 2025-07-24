using BloodDonationSystem.DAL.Repositories.Requests;
using BloodDonationSystem.DAL.Repositories.Responses;
using BloodDonationSystem.DAL.Repositories.UserRepo;
using BusinessObject.Entities;
using BusinessObject.Entities.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace BloodDonationSystem.BLL.Services.UserService;

public class UserService : IUserService
{
    private readonly IUserRepo _userRepo;

    public UserService(IUserRepo userRepo)
    {
        _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
    }

    public async Task<List<User>> GetUsersAsync()
        => await _userRepo.GetUsersAsync();

    public async Task<User?> GetByEmailAndPasswordAsync(string email, string password)
        => await _userRepo.GetByEmailAndPasswordAsync(email, password);

    public Task<List<User>> SearchAsync(string keyword)
    {
        return _userRepo.SearchAsync(keyword);
    }

    public async Task Register(RegisterRequest request)
    {
        await _userRepo.Register(request); // <- MUST HAVE await
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

    public async Task UpdateUserAsync(UpdateUserRequest user)
    {
        await _userRepo.UpdateUserAsync(user);
    }

    public async Task DeleteUserAsync(Guid userID)
    {
        await _userRepo.DeleteUserAsync(userID);
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _userRepo.GetUserByIdAsync(id);
    }

    public async Task<List<User>> GetUserByNameAsync(string userName)
    {
        return await _userRepo.GetUserByNameAsync(userName);
    }

    public async Task<List<User>> GetUsersAsyncHavePagination()
    {
        return await _userRepo.GetUsersAsyncHavePagination();
    }
}