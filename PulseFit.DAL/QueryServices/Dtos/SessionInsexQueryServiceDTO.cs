namespace PulseFit.DAL.QueryServices.Dtos
{
    public class SessionInsexQueryServiceDTO
    {
        public int Id { get; init; }

        public string Speciality { get; init; } = null!;

        public string Description { get; init; } = null!;

        public string TrainerName { get; init; } = null!;

        public DateTime StartDate { get; init; }

        public DateTime EndDate { get; init; }

        public int BookedCount { get; init; }

        public int Capacity { get; init; }
    }
}
