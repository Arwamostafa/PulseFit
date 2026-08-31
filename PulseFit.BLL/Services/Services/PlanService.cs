using Mapster;
using PulseFit.BLL.Models;
using PulseFit.BLL.ModelViews;
using PulseFit.BLL.Services.Contracts;
using PulseFit.DAL.Entities;
using PulseFit.DAL.Repositories.Interfaces;

namespace PulseFit.BLL.Services.Services;

public class PlanService(IUnitOfWork unitOfWork) : IPlanService
{
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<Results<IReadOnlyList<PlanModelView>>> GetAllPlans(CancellationToken cancellationToken)
    {
        var plans = await unitOfWork.GetRepository<Plan>().ListAsync(cancellationToken: cancellationToken);
        if (plans == null) return Results<IReadOnlyList<PlanModelView>>.Failure("Plans not found", "NotFound");

        var plansViews = plans.Adapt<IReadOnlyList<PlanModelView>>();

        return Results<IReadOnlyList<PlanModelView>>.Success(plansViews);
    }

    public async Task<Results<PlanModelView>> GetPlanById(int Id, CancellationToken cancellationToken)
    {
        var plan = await unitOfWork.GetRepository<Plan>().FindByIdAsync(Id, cancellationToken);
        if (plan == null) return Results<PlanModelView>.Failure("Plan not found", "NotFound");
        var Plan = plan.Adapt<PlanModelView>();
        return Results<PlanModelView>.Success(Plan);

    }

    public async Task<Results<PlanToUpdateViewModel>> GetPlanToUpdate(int id, CancellationToken cancellationToken)
    {
        var plan = await unitOfWork.GetRepository<Plan>().FindByIdAsync(id, cancellationToken);
        if (plan is null)
            return Results<PlanToUpdateViewModel>.Failure("Plan not found", "NotFound");

        //var memberShip = await HasMemberShip(id, cancellationToken);
        //if (memberShip)
        //    return Results<PlanToUpdateViewModel>.Failure("Plan Still Has Memberships", "BadRequest");

        return Results<PlanToUpdateViewModel>.Success(plan.Adapt<PlanToUpdateViewModel>());
    }

    private async Task<bool> HasMemberShip(int PlanId, CancellationToken cancellationToken)
    {
        var memperships = await unitOfWork.GetRepository<MemberShip>().ListAsync(Predicate: m => m.PlanId == PlanId && m.Status == "Active", cancellationToken: cancellationToken);

        return memperships.Any();

    }

    public async Task<Results<bool>> UpdatePlan(int id, PlanToUpdateViewModel planModelView, CancellationToken cancellationToken)
    {
        var plan = await unitOfWork.GetRepository<Plan>().FindByIdAsync(id: id, cancellationToken: cancellationToken);
        if (plan is null) return Results<bool>.Failure("Plan not found or has memberships", "NotFound");
        planModelView.Adapt(plan);
        plan.UpdatedAt = DateTime.UtcNow;

        unitOfWork.GetRepository<Plan>().Update(plan);
        var save = await unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken) > 0;
        return Results<bool>.Success(save);
    }

    public async Task<Results<bool>> ToggleActive(int id, CancellationToken cancellationToken)
    {
        var plan = await unitOfWork.GetRepository<Plan>().FindByIdAsync(id, cancellationToken);
        if (plan is null) return Results<bool>.Failure("Plan not found", "NotFound");

        plan.IsActive = !plan.IsActive;
        plan.UpdatedAt = DateTime.UtcNow;

        unitOfWork.GetRepository<Plan>().Update(plan);
        var save = await unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        return Results<bool>.Success(save);
    }
}

