using PulseFit.DAL.Entities;
namespace PulseFit.DAL.Repositories.Interfaces
{

    public interface IMemberRepository
    {
        public Task<bool> IsEmailExist(string email, CancellationToken cancellationToken);
        public Task<bool> IsPhoneExist(string phone, CancellationToken cancellationToken);

        public Task<Member?> GetWirhActiveMemberShip(int memberId, DateTime today, CancellationToken cancellationToken);

    }
}

