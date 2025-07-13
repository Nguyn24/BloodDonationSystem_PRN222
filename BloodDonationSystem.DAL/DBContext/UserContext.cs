using System.Security.Claims;
using BloodDonationSystem.DAL.Shared;
using Microsoft.AspNetCore.Http;

namespace BloodDonationSystem.DAL.DBContext;

public class UserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public Guid UserId =>
        _httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new ApplicationException("User context is unavailable");
    
}
