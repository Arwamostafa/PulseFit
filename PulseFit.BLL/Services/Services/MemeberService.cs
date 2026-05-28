using PulseFit.BLL.Services.Contracts;
using PulseFit.DAL.Entities;
using PulseFit.DAL.Repositories.Interfaces;

namespace PulseFit.BLL.Services.Services;

public class MemeberService(IGenaricRepository<Member> genericRepository) : IMemberService
{
    public async Task<IEnumerable<Member>> ListMembersAsync()
    {
        var members = await genericRepository.GetAllAsync();
        if (members == null || !members.Any()) return [];
        var Members = members.Select(m => new Member
        {
            Id = m.Id,
            Name = m.Name,
            Email = m.Email,
            PhoneNumber = m.PhoneNumber,
            Photo = m.Photo,
            HealthRecored = m.HealthRecored,
            MemberShips = m.MemberShips,
            MemberSessions = m.MemberSessions
        }).ToList();
        return Members;
    }
}

