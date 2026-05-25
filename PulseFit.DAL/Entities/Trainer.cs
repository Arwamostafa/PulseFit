using PulseFit.DAL.Enums;

namespace PulseFit.DAL.Entities;

public class Trainer : User
{
    public Specialties Specialties { get; set; }
    public ICollection<Session> Sessions { get; set; } = [];

}

