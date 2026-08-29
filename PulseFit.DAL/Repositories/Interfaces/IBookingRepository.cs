namespace PulseFit.DAL.Repositories.Interfaces;

public interface IBookingRepository
{
    public Task<bool> HasUpcomingBookingWithWithMemeberAsync(int Memberid, DateTime date, CancellationToken ct);
}

