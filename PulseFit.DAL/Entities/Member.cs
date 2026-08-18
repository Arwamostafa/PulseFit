
namespace PulseFit.DAL.Entities;

public class Member : User
{
    public string? Photo { get; set; } = null!;
    public DateTime JoinDate { get; set; } = default!;

    public HealthRecored HealthRecored { get; set; } = null!;

    public ICollection<MemberShip> MemberShips { get; set; } = [];

    public ICollection<MemberSession> MemberSessions { get; set; } = [];

    public ICollection<Booking> Bookings { get; set; } = [];
}

