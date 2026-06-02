using PulseFit.BLL.ModelViews;
using PulseFit.DAL.Entities;

namespace PulseFit.BLL.Services.Contracts
{
    public interface IMemberService
    {
        public Task<IEnumerable<Member>> ListMembersAsync();

        public Task<bool> CreateMemberAsync(CreateMemberViewModel member);

        public Task<MemberModelView>? GetByIdAsync(int id);

        public Task<HealthRecordViewModel>? GetHealthRecordAsync(int id);

        public Task<MemberToUpdateViewModel>? GetMemberToUpdateAsync(int id);

        public Task<bool> UpdateMemberAsync(int id, MemberToUpdateViewModel member);
        public Task<bool> DeleteMemberAsync(int id);
    }
}
