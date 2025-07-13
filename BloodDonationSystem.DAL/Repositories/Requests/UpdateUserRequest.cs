using BusinessObject.Entities.Enum;

namespace BloodDonationSystem.DAL.Repositories.Requests;

public class UpdateUserRequest
{
    public Guid UserId { get; set; } 
    public string? FullName { get; set; } 
    public string? Email { get; set; } 
    public UserRole? Role { get; set; }
    public UserStatus? Status { get; set; }
    public string? BloodType { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public UserGender? Gender { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public bool? IsDonor { get; set; }
}