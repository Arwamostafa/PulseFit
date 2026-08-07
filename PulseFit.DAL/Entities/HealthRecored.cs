using PulseFit.DAL.Enums;

namespace PulseFit.DAL.Entities;

public class HealthRecored : BaseEntity
{
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public BloodType BloodType { get; set; }
    public string? Note { get; set; }


}

