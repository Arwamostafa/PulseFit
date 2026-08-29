using Mapster;
using Microsoft.EntityFrameworkCore;
using PulseFit.BLL.Models;
using PulseFit.BLL.ModelViews;
using PulseFit.BLL.Services.Contracts;
using PulseFit.DAL.Entities;
using PulseFit.DAL.Enums;
using PulseFit.DAL.Repositories.Interfaces;

namespace PulseFit.BLL.Services.Services;

public class MemeberService(IUnitOfWork unitOfWork) : IMemberService
{
    public async Task<Results> CreateMemberAsync(CreateMemberViewModel member, CancellationToken cancellationToken = default)
    {
        if (await unitOfWork.GetMemberRepository().IsEmailExist(member.Email, cancellationToken))
            return Results.Failure("Email is already registered.", "BadRequest");

        if (await unitOfWork.GetMemberRepository().IsPhoneExist(member.Phone, cancellationToken))
            return Results.Failure("Phone number is already registered.", "BadRequest");

        if (!Enum.TryParse<Gender>(member.Gender, true, out var gender))
            return Results.Failure("Invalid gender.", "BadRequest");

        if (!Enum.TryParse<BloodType>(member.HealthRecord.BloodType, true, out var bloodType))
            return Results.Failure("Invalid blood type.", "BadRequest");

        var memberEntity = member.Adapt<Member>();

        //    new Member
        //{
        //    Email = member.Email,
        //    PhoneNumber = member.Phone,
        //    Name = member.Name,
        //    Gender = gender,
        //    DateOfBirth = member.DateOfBirth,
        //    Address = new Address
        //    {
        //        BuildingNumber = member.BuildingNumber,
        //        Street = member.Street,
        //        City = member.City
        //    },
        //    HealthRecored = new HealthRecored
        //    {
        //        Height = member.HealthRecord.Height,
        //        Weight = member.HealthRecord.Weight,
        //        BloodType = bloodType,
        //        Note = member.HealthRecord.Note
        //    }
        //};

        await unitOfWork.GetRepository<Member>().AddAsync(memberEntity, cancellationToken);
        var rows = await unitOfWork.SaveChangesAsync(cancellationToken);

        return rows > 0 ? Results.Success() : Results.Failure("Failed to create member.", "failur of server");
    }



    public async Task<Results> DeleteMemberAsync(int id, CancellationToken cancellationToken = default)
    {
        var memberEntity = await unitOfWork.GetRepository<Member>().FindAsync(
            Predicate: m => m.Id == id,
            include: m => m.Include(m => m.MemberSessions).Include(m => m.MemberShips),
            cancellationToken: cancellationToken);

        if (memberEntity == null)
            return Results.Failure($"Member with id {id} was not found.", nameof(memberEntity));


        if (await unitOfWork.GetBookingRepository().HasUpcomingBookingWithWithMemeberAsync(memberEntity.Id, DateTime.UtcNow, cancellationToken))
            return Results.Failure("Cannot delete a member with upcoming sessions.", "start date of session");



        await unitOfWork.BeginTrasaction(cancellationToken);


        unitOfWork.GetRepository<HealthRecored>().Delete(memberEntity.HealthRecored);


        unitOfWork.GetRepository<Member>().Delete(memberEntity);

        var rows = await unitOfWork.SaveChangesAsync(cancellationToken);
        if (rows == 0)
            return Results.Failure("Delete failed, no rows were affected.", "ServerError");

        await unitOfWork.CommitAsync(cancellationToken);
        return Results.Success();
    }

    public async Task<Results<MemberModelView?>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var member = await unitOfWork.GetRepository<Member>().FindAsync(Predicate: m => m.Id == id,
                                                           include: m => m.Include(m => m.MemberShips).
                                                           ThenInclude(ms => ms.Plan),
                                                           cancellationToken: cancellationToken);

        if (member == null) return Results<MemberModelView?>.Failure("Member not found.", nameof(member));
        return Results<MemberModelView?>.Success(member.Adapt<MemberModelView>());
    }

    public async Task<Results<HealthRecordViewModel?>> GetHealthRecordAsync(int id, CancellationToken cancellationToken = default)
    {
        var healthRecord = await unitOfWork.GetRepository<HealthRecored>().FindByIdAsync(id, cancellationToken: cancellationToken);

        if (healthRecord == null) return Results<HealthRecordViewModel?>.Failure("Health record not found.", nameof(healthRecord));
        return Results<HealthRecordViewModel?>.Success(healthRecord.Adapt<HealthRecordViewModel>());
    }

    public async Task<Results<MemberToUpdateViewModel?>> GetMemberToUpdateAsync(int id, CancellationToken cancellationToken)
    {
        var member = await unitOfWork.GetRepository<Member>().FindByIdAsync(id, cancellationToken: cancellationToken);

        if (member == null) return Results<MemberToUpdateViewModel?>.Failure("Member not found.", nameof(member));


        return Results<MemberToUpdateViewModel?>.Success(member.Adapt<MemberToUpdateViewModel>());
    }

    public async Task<Results> UpdateMemberAsync(int id, MemberToUpdateViewModel member, CancellationToken cancellationToken)
    {
        var memberEntity = await unitOfWork.GetRepository<Member>().FindAsync(
            Predicate: m => m.Id == id,
            cancellationToken: cancellationToken);

        if (memberEntity == null)
            return Results.Failure($"Member with id {id} was not found.", nameof(Member));

        if (await unitOfWork.GetMemberRepository().IsEmailExist(member.Email, cancellationToken) && memberEntity.Id != id)
            return Results.Failure("Email is already in use.", nameof(member.Email));

        if (await unitOfWork.GetMemberRepository().IsPhoneExist(member.Phone, cancellationToken) && memberEntity.Id != id)
            return Results.Failure("Phone number is already in use.", nameof(member.Phone));

        member.Adapt(memberEntity);
        memberEntity.UpdatedAt = DateTime.UtcNow;

        unitOfWork.GetRepository<Member>().Update(memberEntity);

        var rows = await unitOfWork.SaveChangesAsync(cancellationToken);
        return rows > 0 ? Results.Success() : Results.Failure("Failed to update member.", "server error");
    }

    public async Task<Results<IEnumerable<MemberModelView>>> ListMembersAsync(CancellationToken cancellationToken)
    {
        var members = await unitOfWork.GetRepository<Member>().ListAsync();
        //if (members == null || !members.Any()) return Results.NotFound("No members found.");
        var Members = members.Adapt<IEnumerable<MemberModelView>>();
        return Results<IEnumerable<MemberModelView>>.Success(Members);
    }

    //private async Task<bool> ExistEmail(string email, CancellationToken cancellationToken) =>
    //    await unitOfWork.GetRepository<Member>().FindAsync(Predicate: m => m.Email == email, cancellationToken: cancellationToken) != null;

    //private async Task<bool> ExistPhone(string phone, CancellationToken cancellationToken) =>
    //    await unitOfWork.GetRepository<Member>().FindAsync(Predicate: m => m.PhoneNumber == phone, cancellationToken: cancellationToken) != null;

}

