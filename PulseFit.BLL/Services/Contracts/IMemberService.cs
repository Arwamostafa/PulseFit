using PulseFit.BLL.Models;
using PulseFit.BLL.ModelViews;

namespace PulseFit.BLL.Services.Contracts
{
    public interface IMemberService
    {
        public Task<Results<IReadOnlyList<MemberModelView>>> ListMembersAsync(CancellationToken cancellationToken);

        public Task<result> CreateMemberAsync(CreateMemberViewModel member, CancellationToken cancellationToken = default);

        public Task<Results<MemberModelView?>> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        public Task<Results<HealthRecordViewModel?>> GetHealthRecordAsync(int id, CancellationToken cancellationToken = default);

        public Task<Results<MemberToUpdateViewModel?>> GetMemberToUpdateAsync(int id, CancellationToken cancellationToken = default);

        public Task<result> UpdateMemberAsync(int id, MemberToUpdateViewModel member, CancellationToken cancellationToken);

        public Task<result> DeleteMemberAsync(int id, CancellationToken cancellationToken = default);
    }
}
