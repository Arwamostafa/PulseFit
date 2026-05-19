

namespace PulseFit.DAL.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;
    public ICollection<Session> Sessions { get; set; } = [];
}

