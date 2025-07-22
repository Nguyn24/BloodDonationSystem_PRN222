using System.ComponentModel.DataAnnotations;

namespace BloodDonationSystem.DAL.Repositories.Requests;

public class UpdateBloodStoredRequest
{
    public Guid StoredId { get; set; }

    
    public Guid BloodTypeId { get; set; } = default!;

    [Required(ErrorMessage = "Quantity is required.")]
    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
    public int? Quantity { get; set; }
}