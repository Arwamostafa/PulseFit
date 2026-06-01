using PulseFit.BLL.ModelViews;
using PulseFit.BLL.Services.Contracts;
using PulseFit.DAL.Entities;
using PulseFit.DAL.Repositories.Interfaces;

namespace PulseFit.BLL.Services.Services;

public class MemeberService(IGenaricRepository<Member> genericRepository, CancellationToken cancellationToken) : IMemberService
{
    public async Task<bool> CreateMemberAsync(CreateMemberViewModel member)
    {

        var ExsitEmail = await genericRepository.GetByIdAsync(Predicate: m => m.Email == member.Email, cancellationToken: cancellationToken);
        if (ExsitEmail != null) return false;

        var ExistPhone = await genericRepository.GetByIdAsync(Predicate: m => m.PhoneNumber == member.Phone, cancellationToken: cancellationToken);
        if (ExistPhone != null) return false;

        var memberEntity = new Member
        {
            Email = member.Email,
            PhoneNumber = member.Phone,
            Name = member.Name,
            Gender = member.Gender,
            DateOfBirth = member.DateOfBirth,
            Address = new Address
            {
                BuildingNumber = member.BuildingNumber,
                Street = member.Street,
                City = member.City
            },
            HealthRecored = new HealthRecored
            {
                Height = member.HealthRecord.Height,
                Weight = member.HealthRecord.Weight,
                BloodType = member.HealthRecord.BloodType,
                Note = member.HealthRecord.Note

            }

        };
        await genericRepository.AddAsync(memberEntity);
        return await genericRepository.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<IEnumerable<Member>> ListMembersAsync()
    {
        var members = await genericRepository.ListAsync();
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

