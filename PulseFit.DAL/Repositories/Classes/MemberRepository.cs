using Microsoft.EntityFrameworkCore;
using PulseFit.DAL.Entities;
using PulseFit.DAL.Repositories.Interfaces;

namespace PulseFit.DAL.Repositories.Classes
{
    public class MemberRepository(PluseFitDbContext context) : GenaricRepository<Member>(context), IMemberRepository
    {
        public PluseFitDbContext _context { get; } = context;

        public async Task<Member?> GetWirhActiveMemberShip(int memberId, DateTime today, CancellationToken cancellationToken)
        => await _context.Set<Member>().Include(m => m.MemberShips.Where(ms => ms.StartDate <= today && ms.EndDate >= today))
                                      .ThenInclude(m => m.Plan)
                                      .FirstOrDefaultAsync(m => m.Id == memberId);

        public async Task<bool> IsEmailExist(string email, CancellationToken cancellationToken)
        => await _context.Set<Member>().AnyAsync(m => m.Email == email);

        public async Task<bool> IsPhoneExist(string phone, CancellationToken cancellationToken)
        => await _context.Set<Member>().AnyAsync(m => m.PhoneNumber == phone);
    }
}
