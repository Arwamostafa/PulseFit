using PulseFit.BLL.ModelViews.Enums;

namespace PulseFit.BLL.ModelViews;

public class SessionViewModel
{
    public int Id { get; init; }

    public string Speciality { get; init; } = null!;

    public string Description { get; init; } = null!;

    public string TrainerName { get; init; } = null!;

    public DateTime StartDate { get; init; }

    public DateTime EndDate { get; init; }

    public int BookedCount { get; init; }

    public int Capacity { get; init; }

    public SessionStatus Status { get; init; }

    public TimeSpan Duration => EndDate - StartDate;

    public string HeaderClass => Status switch
    {

        SessionStatus.Upcoming => "bg-primary",
        SessionStatus.Ongoing => "bg-success",
        SessionStatus.Completed => "bg-secondary",
        _ => "bg-secondary"

    };
}

