using Microsoft.EntityFrameworkCore;
using PulseFit.BLL.Models;
using PulseFit.BLL.ModelViews;
using PulseFit.BLL.Services.Contracts;
using PulseFit.DAL.Entities;
using PulseFit.DAL.Repositories.Interfaces;

namespace PulseFit.BLL.Services.Services;

public class MemeberService(IUnitOfWork unitOfWork) : IMemberService
{
    public async Task<Results> CreateMemberAsync(CreateMemberViewModel member, CancellationToken cancellationToken = default)
    {
        if (await ExistEmail(member.Email, cancellationToken))
            return Results.BadRequest("Email is already registered.");

        if (await ExistPhone(member.Phone, cancellationToken))
            return Results.BadRequest("Phone number is already registered.");

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

        await unitOfWork.GetRepository<Member>().AddAsync(memberEntity, cancellationToken);
        var rows = await unitOfWork.SaveChangesAsync(cancellationToken);

        return rows > 0 ? Results.Success() : Results.ServerError("Failed to create member.");
    }

    public async Task<Results> DeleteMemberAsync(int id, CancellationToken cancellationToken = default)
    {
        var memberEntity = await unitOfWork.GetRepository<Member>().FindAsync(
            Predicate: m => m.Id == id,
            include: m => m.Include(m => m.MemberSessions).Include(m => m.MemberShips),
            cancellationToken: cancellationToken);

        if (memberEntity == null)
            return Results.NotFound($"Member with id {id} was not found.");

        if (memberEntity.MemberSessions.Any(s => s.Session.StartDate > DateTime.Now))
            return Results.BadRequest("Cannot delete a member with upcoming sessions.");

        await unitOfWork.BeginTrasaction(cancellationToken);

        if (memberEntity.MemberShips.Any())
        {
            foreach (var memberShip in memberEntity.MemberShips)
                unitOfWork.GetRepository<MemberShip>().Delete(memberShip);
        }

        unitOfWork.GetRepository<Member>().Delete(memberEntity);

        var rows = await unitOfWork.SaveChangesAsync(cancellationToken);
        if (rows == 0)
            return Results.ServerError("Delete failed, no rows were affected.");

        await unitOfWork.CommitAsync(cancellationToken);
        return Results.Success();
    }

    public async Task<Results<MemberModelView?>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var member = await unitOfWork.GetRepository<Member>().FindAsync(Predicate: m => m.Id == id,
                                                           include: m => m.Include(m => m.MemberShips).
                                                           ThenInclude(ms => ms.Plan),
                                                           cancellationToken: cancellationToken);

        if (member == null) return Results.NotFound("Member not found.");
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
        return Results<MemberModelView?>.Success(memberModelView);
    }

    public async Task<Results<HealthRecordViewModel?>> GetHealthRecordAsync(int id, CancellationToken cancellationToken = default)
    {
        var healthRecord = await unitOfWork.GetRepository<HealthRecored>().FindByIdAsync(id, cancellationToken: cancellationToken);

        if (healthRecord == null) return Results.NotFound("Health record not found.");
        var healthRecordViewModel = new HealthRecordViewModel
        {
            Height = healthRecord.Height,
            Weight = healthRecord.Weight,
            BloodType = healthRecord.BloodType,
            Note = healthRecord.Note
        };
        return Results<HealthRecordViewModel?>.Success(healthRecordViewModel);
    }

    public async Task<Results<MemberToUpdateViewModel?>> GetMemberToUpdateAsync(int id, CancellationToken cancellationToken)
    {
        var member = await unitOfWork.GetRepository<Member>().FindByIdAsync(id, cancellationToken: cancellationToken);

        if (member == null) return Results.NotFound("Member not found.");

        var memberToUpdateViewModel = new MemberToUpdateViewModel
        {
            Name = member.Name,
            Email = member.Email,
            Phone = member.PhoneNumber,
            BuildingNumber = member.Address.BuildingNumber,
            Street = member.Address.Street,
            City = member.Address.City,
        };
        return Results<MemberToUpdateViewModel?>.Success(memberToUpdateViewModel);
    }

    public async Task<Results> UpdateMemberAsync(int id, MemberToUpdateViewModel member, CancellationToken cancellationToken)
    {
        var memberEntity = await unitOfWork.GetRepository<Member>().FindAsync(
            Predicate: m => m.Id == id,
            cancellationToken: cancellationToken);

        if (memberEntity == null)
            return Results.NotFound($"Member with id {id} was not found.");

        if (await ExistEmail(member.Email, cancellationToken) && memberEntity.Id != id)
            return Results.BadRequest("Email is already in use.");

        if (await ExistPhone(member.Phone, cancellationToken))
            return Results.BadRequest("Phone number is already in use.");

        memberEntity.Name = member.Name;
        memberEntity.Email = member.Email;
        memberEntity.PhoneNumber = member.Phone;
        memberEntity.Address.BuildingNumber = member.BuildingNumber;
        memberEntity.Address.Street = member.Street;
        memberEntity.Address.City = member.City;
        memberEntity.UpdatedAt = DateTime.UtcNow;
        memberEntity.Photo = member.Photo;

        unitOfWork.GetRepository<Member>().Update(memberEntity);

        var rows = await unitOfWork.SaveChangesAsync(cancellationToken);
        return rows > 0 ? Results.Success() : Results.ServerError("Failed to update member.");
    }

    public async Task<Results<IEnumerable<Member>>> ListMembersAsync(CancellationToken cancellationToken)
    {
        var members = await unitOfWork.GetRepository<Member>().ListAsync();
        if (members == null || !members.Any()) return Results.NotFound("No members found.");
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
        return Results<IEnumerable<Member>>.Success(Members);
    }

    private async Task<bool> ExistEmail(string email, CancellationToken cancellationToken) =>
        await unitOfWork.GetRepository<Member>().FindAsync(Predicate: m => m.Email == email, cancellationToken: cancellationToken) != null;

    private async Task<bool> ExistPhone(string phone, CancellationToken cancellationToken) =>
        await unitOfWork.GetRepository<Member>().FindAsync(Predicate: m => m.PhoneNumber == phone, cancellationToken: cancellationToken) != null;

}

