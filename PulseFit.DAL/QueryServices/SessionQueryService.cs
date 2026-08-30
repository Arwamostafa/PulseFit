using Microsoft.EntityFrameworkCore;
using PulseFit.DAL.QueryServices.Dtos;

namespace PulseFit.DAL.QueryServices
{
    public class SessionQueryService(PluseFitDbContext pluseFitDbContext) : ISessionQueryService
    {
        public async Task<IReadOnlyList<SessionInsexQueryServiceDTO>> Index(CancellationToken cancellationToken)
        {
            return await pluseFitDbContext.Sessions.
                                 AsNoTracking()
                                 .Select(s => new SessionInsexQueryServiceDTO
                                 {
                                     Id = s.Id,
                                     Description = s.Description,
                                     Speciality = s.Trainer.Specialties.ToString(),
                                     StartDate = s.StartDate,
                                     EndDate = s.EndDate,
                                     TrainerName = s.Trainer.Name,
                                     BookedCount = s.Bookings.Count,
                                     Capacity = s.Capacity
                                 }).ToListAsync(cancellationToken);

        }
    }
}
