using BloodDonationSystem.DAL.Repositories.Requests;
using BloodDonationSystem.DAL.Repositories.Responses;
using BusinessObject.Entities;

namespace BloodDonationSystem.DAL.Repositories.UserRepo;

public interface IUserRepo
{
    Task<List<User>> GetUsersAsyncHavePagination();
    Task<List<BusinessObject.Entities.User>> GetUsersAsync();
    Task<GetCurrentUserResponse> GetCurrentUserAsync();
    Task<List<BusinessObject.Entities.User>> GetUserByNameAsync(string userName);
    Task CreateUserAsync(CreateUsersRequest request);
    Task<BusinessObject.Entities.User?> GetUserByIdAsync(Guid id);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(Guid userID);
    Task<BusinessObject.Entities.User?> GetByEmailAndPasswordAsync(string email, string password);
    Task<List<BusinessObject.Entities.User>> SearchAsync(string keyword);
    Task Register(RegisterRequest request);
}