using PulseFit.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace PulseFit.BLL.ModelViews;

public class HealthRecordViewModel
{
    [Required]
    [Range(0.1, 300, ErrorMessage = "Height must be between 0.1 and 300 cm")]
    public decimal Height { get; set; }

    [Required]
    [Range(1, 500, ErrorMessage = "Weight must be between 1 and 500 kg")]
    public decimal Weight { get; set; }

    [Required]
    public BloodType BloodType { get; set; }

    public string? Note { get; set; }
}

