using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PulseFit.DAL.Entities;
using PulseFit.DAL.Enums;
using PulseFit.DAL.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PulseFit.DAL.Repositories.Classes
{
    public class MemberRepository(PluseFitDbContext context) :GenaricRepository<Member>(context) ,IMemberRepository
    {
        public async Task<Member?> ActiveMemberShip(int memberId , DateTime today , CancellationToken cancellationToken)
        => await context.Set<Member>().Include(m=>m.MemberShips.Where(ms => ms.StartDate <= today && ms.EndDate >= today)).FirstOrDefaultAsync(m => m.Id == memberId);

        public async Task<bool> IsEmailExist(string email , CancellationToken cancellationToken)
        => await context.Set<Member>().AnyAsync(m=>m.Email == email);

        public async Task<bool> IsPhoneExist(string phone , CancellationToken cancellationToken)
        => await context.Set<Member>().AnyAsync(m => m.PhoneNumber == phone);
    }
}
