using BloodDonationSystem.DAL.Repositories.Requests;
using BloodDonationSystem.DAL.Repositories.Responses;
using BusinessObject.Entities;

namespace BloodDonationSystem.BLL.Services.UserService;

public interface IUserService
{
    Task<List<User>> GetUsersAsyncHavePagination();
    Task<List<User>> GetUsersAsync();
    Task<GetCurrentUserResponse> GetCurrentUserAsync();
    Task<List<User>> GetUserByNameAsync(string userName);
    Task CreateUserAsync(CreateUsersRequest request);
    Task<User?> GetUserByIdAsync(Guid id);
    Task UpdateUserAsync(UpdateUserRequest user);
    Task DeleteUserAsync(Guid userID);
    Task<User?> AuthenticateAsync(string email, string password);
    Task<List<User>> SearchAsync(string keyword);
    Task Register(RegisterRequest request);


}