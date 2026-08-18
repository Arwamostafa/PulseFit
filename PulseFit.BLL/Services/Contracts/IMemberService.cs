using PulseFit.BLL.Models;
using PulseFit.BLL.ModelViews;
using PulseFit.DAL.Entities;

namespace PulseFit.BLL.Services.Contracts
{
    public interface IMemberService
    {
        public Task<Results<IEnumerable<MemberModelView>>> ListMembersAsync(CancellationToken cancellationToken);

        public Task<Results> CreateMemberAsync(CreateMemberViewModel member, CancellationToken cancellationToken = default);

        public Task<Results<MemberModelView?>> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        public Task<Results<HealthRecordViewModel?>> GetHealthRecordAsync(int id, CancellationToken cancellationToken = default);

        public Task<Results<MemberToUpdateViewModel?>> GetMemberToUpdateAsync(int id, CancellationToken cancellationToken = default);

        public Task<Results> UpdateMemberAsync(int id, MemberToUpdateViewModel member, CancellationToken cancellationToken);

        public Task<Results> DeleteMemberAsync(int id, CancellationToken cancellationToken = default);
    }
}
