using Microsoft.EntityFrameworkCore;
using PulseFit.BLL.ModelViews;
using PulseFit.BLL.Services.Contracts;
using PulseFit.DAL.Entities;
using PulseFit.DAL.Repositories.Interfaces;

namespace PulseFit.BLL.Services.Services;

public class MemeberService(IGenaricRepository<Member> MemberRepository, IGenaricRepository<HealthRecored> healthRecordRepository, IGenaricRepository<MemberShip> memberShipRepository, CancellationToken cancellationToken) : IMemberService
{
    private readonly IGenaricRepository<Member> _MemberRepository = MemberRepository;
    private readonly IGenaricRepository<HealthRecored> _healthRecordRepository = healthRecordRepository;
    private readonly IGenaricRepository<MemberShip> _memberShipRepository = memberShipRepository;

    public async Task<bool> CreateMemberAsync(CreateMemberViewModel member)
    {

        if (await ExistEmail(member.Email) || await ExistPhone(member.Phone)) return false;

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
        await _MemberRepository.AddAsync(memberEntity);
        return await _MemberRepository.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteMemberAsync(int id)
    {
        var memberEntity = await _MemberRepository.FindAsync(Predicate: m => m.Id == id && m.MemberSessions.Any(m => m.Session.StartDate > DateTime.Now), include: m => m.Include(m => m.MemberSessions).Include(m => m.MemberShips), cancellationToken: cancellationToken);
        if (memberEntity != null) return false;
        var hasMemberSession = memberEntity;

        if (memberEntity.MemberShips.Any())
        {
            foreach (var memberShip in memberEntity.MemberShips)
            {
                _memberShipRepository.Delete(memberShip);
            }
        }
        _MemberRepository.Delete(memberEntity);
        return await _MemberRepository.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<MemberModelView>? GetByIdAsync(int id)
    {
        var member = await _MemberRepository.FindAsync(Predicate: m => m.Id == id,
                                                           include: m => m.Include(m => m.MemberShips).
                                                           ThenInclude(ms => ms.Plan),
                                                           cancellationToken: cancellationToken);

        if (member == null) return null;
        var memberModelView = new MemberModelView
        {
            Id = member.Id,
            Name = member.Name,
            Email = member.Email,
            Phone = member.PhoneNumber,
            Photo = member.Photo,
            Gender = member.Gender.ToString(),
            PlanName = member.MemberShips.Select(ms => ms.Plan.Name).FirstOrDefault(),
            MembershipStartDate = member.MemberShips.Select(ms => ms.CreatedAt.ToString("yyyy-MM-dd")).FirstOrDefault(),
            MembershipEndDate = member.MemberShips.Select(ms => ms.EndDate.ToString("yyyy-MM-dd")).FirstOrDefault(),
            Address = $"{member.Address.BuildingNumber} {member.Address.Street} {member.Address.City}",

        };
        return memberModelView;
    }

    public async Task<HealthRecordViewModel>? GetHealthRecordAsync(int id)
    {
        var healthRecord = await _healthRecordRepository.FindByIdAsync(id, cancellationToken: cancellationToken);

        if (healthRecord == null) return null;
        var healthRecordViewModel = new HealthRecordViewModel
        {
            Height = healthRecord.Height,
            Weight = healthRecord.Weight,
            BloodType = healthRecord.BloodType,
            Note = healthRecord.Note
        };
        return healthRecordViewModel;

    }

    public async Task<MemberToUpdateViewModel>? GetMemberToUpdateAsync(int id)
    {
        var member = await _MemberRepository.FindByIdAsync(id, cancellationToken: cancellationToken);

        if (member == null) return null;

        var memberToUpdateViewModel = new MemberToUpdateViewModel
        {
            Name = member.Name,
            Email = member.Email,
            Phone = member.PhoneNumber,
            BuildingNumber = member.Address.BuildingNumber,
            Street = member.Address.Street,
            City = member.Address.City,
        };
        return memberToUpdateViewModel;
    }

    public async Task<bool> UpdateMemberAsync(int id, MemberToUpdateViewModel member)
    {
        if (await ExistEmail(member.Email) && await ExistPhone(member.Phone)) return false;

        var memberEntity = await _MemberRepository.FindByIdAsync(id, cancellationToken: cancellationToken);
        if (memberEntity == null) return false;

        memberEntity.Name = member.Name;
        memberEntity.Email = member.Email;
        memberEntity.PhoneNumber = member.Phone;
        memberEntity.Address.BuildingNumber = member.BuildingNumber;
        memberEntity.Address.Street = member.Street;
        memberEntity.Address.City = member.City;
        memberEntity.UpdatedAt = DateTime.UtcNow;
        memberEntity.Photo = member.Photo;

        _MemberRepository.Update(memberEntity);

        return await _MemberRepository.SaveChangesAsync(cancellationToken) > 0;


    }

    public async Task<IEnumerable<Member>> ListMembersAsync()
    {
        var members = await _MemberRepository.ListAsync();
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

    private async Task<bool> ExistEmail(string email) => _MemberRepository.FindAsync(Predicate: m => m.Email == email, cancellationToken: cancellationToken) != null;

    private async Task<bool> ExistPhone(string phone) => _MemberRepository.FindAsync(Predicate: m => m.PhoneNumber == phone, cancellationToken: cancellationToken) != null;


}

