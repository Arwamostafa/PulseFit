namespace PulseFit.DAL.Entities;
public class Session
{
    public string Description { get; set; } = null!;
    public int Capacity { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

}

