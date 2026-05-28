using PulseFit.DAL.Entities;

namespace PulseFit.BLL.Services.Contracts
{
    public interface IMemberService
    {
        public Task<IEnumerable<Member>> ListMembersAsync();
    }
}
