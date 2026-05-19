namespace PulseFit.DAL.Entities;

public class HealthRecored : BaseEntity
{
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public string BloodType { get; set; } = null!;
    public string? Note { get; set; }


}

