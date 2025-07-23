using System.ComponentModel.DataAnnotations;

namespace BloodDonationSystem.DAL.Repositories.Requests;

public class UpdateDonorRequest
{
    public Guid DonorInfoId { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Weight must be greater than 0.")]
    public decimal? Weight { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Height must be greater than 0.")]
    public decimal? Height { get; set; }

    [Required]
    public string? MedicalStatus { get; set; }

    public DateTime LastChecked { get; set; }
}