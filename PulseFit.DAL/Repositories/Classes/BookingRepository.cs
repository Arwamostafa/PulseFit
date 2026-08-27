using Microsoft.EntityFrameworkCore;
using PulseFit.DAL.Entities;
using PulseFit.DAL.Repositories.Interfaces;

namespace PulseFit.DAL.Repositories.Classes;

public class BookingRepository(PluseFitDbContext pluseFitDbContext) : GenaricRepository<Booking>(pluseFitDbContext), IBookingRepository
{
    public PluseFitDbContext _pluseFitDbContext { get; } = pluseFitDbContext;

    public async Task<bool> HasUpcomingBookingWithWithMemeberAsync(int Memberid, DateTime date, CancellationToken ct)
     => await _pluseFitDbContext.Bookings.AnyAsync(b => b.MemberId == Memberid && b.Session.EndDate >= date, ct);
}

