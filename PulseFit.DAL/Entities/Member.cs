
namespace PulseFit.DAL.Entities;

public class Member : User
{
    public string? Photo { get; set; } = null!;

    public HealthRecored HealthRecored { get; set; } = null!;

}

