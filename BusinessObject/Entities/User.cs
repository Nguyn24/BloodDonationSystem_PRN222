using BusinessObject.Entities.Enum;

namespace BusinessObject.Entities;

public partial class User
{
    public Guid UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public UserGender Gender { get; set; } 

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public UserRole Role { get; set; }

    public bool? IsDonor { get; set; }

    public DateTimeOffset? LastDonationDate { get; set; }

    public UserStatus Status { get; set; } 

    public Guid? BloodTypeId { get; set; }

    public string? ImageUrl { get; set; }
    
    public BloodType? BloodType { get; set; }         
    
    public virtual ICollection<DonationRequest> DonationRequests { get; set; } = new List<DonationRequest>();

    public virtual ICollection<DonationsHistory> DonationsHistoryConfirmedByNavigations { get; set; } = new List<DonationsHistory>();

    public virtual ICollection<DonationsHistory> DonationsHistoryUsers { get; set; } = new List<DonationsHistory>();

    public virtual DonorInformation? DonorInformation { get; set; }
}
