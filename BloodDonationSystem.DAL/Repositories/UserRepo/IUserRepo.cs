using BloodDonationSystem.DAL.Repositories.Requests;
using BloodDonationSystem.DAL.Repositories.Responses;

namespace BloodDonationSystem.DAL.Repositories.UserRepo;

public interface IUserRepo
{
    Task<List<BusinessObject.Entities.User>> GetUsersAsync();
    Task<GetCurrentUserResponse> GetCurrentUserAsync();
    Task<List<BusinessObject.Entities.User>> GetUserByNameAsync(string userName);
    Task CreateUserAsync(CreateUsersRequest request);
    Task<BusinessObject.Entities.User?> GetUserByIdAsync(Guid id);
    Task UpdateUserAsync(UpdateUserRequest request);
    Task DeleteUserAsync(BusinessObject.Entities.User user);
    Task<BusinessObject.Entities.User?> GetByEmailAndPasswordAsync(string email, string password);
    Task<List<BusinessObject.Entities.User>> SearchAsync(string keyword);
    Task Register(RegisterRequest request);
}