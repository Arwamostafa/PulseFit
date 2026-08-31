namespace PulseFit.BLL.ModelViews.Enums;

public class SessionDetailsViewModel
{
    public string Speciality { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string TrainerName { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int BookedCount { get; set; }

    public int MaxCapacity { get; set; }

    public string Capacity => $"{BookedCount} / {MaxCapacity} Spots";

    public SessionStatus Status { get; set; }

    public string Duration
    {
        get
        {
            var tempstamp = EndDate - StartDate;
            return $"{tempstamp.Hours} Houres {tempstamp.Seconds}Seconds";

        }
    }
    public string HeaderClass => Status switch
    {
        SessionStatus.Upcoming => "bg-primary",
        SessionStatus.Ongoing => "bg-success",
        SessionStatus.Completed => "bg-secondary",
        _ => "bg-secondary"
    };
}

