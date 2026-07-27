using PulseFit.BLL.Models;
using PulseFit.BLL.ModelViews;
using PulseFit.BLL.Services.Contracts;
using PulseFit.DAL.Entities;
using PulseFit.DAL.Repositories.Interfaces;

namespace PulseFit.BLL.Services.Services;

public class PlanService(IUnitOfWork unitOfWork) : IPlanService
{
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<Results<IEnumerable<PlanModelView>>> GetAllPlans(CancellationToken cancellationToken)
    {
        var plans = await unitOfWork.GetPlanRepository().GetAllAsync(cancellationToken);
        if (plans == null) return Results.NotFound("");

        var plansViews = plans.Select(p => new PlanModelView
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            DurationDays = p.DurationInDays,
            Price = p.Price,
            IsActive = p.IsActive,
        });

        return Results<IEnumerable<PlanModelView>>.Success(plansViews);
    }

    public async Task<Results<PlanModelView>> GetPlanById(int Id, CancellationToken cancellationToken)
    {
        var plan = await unitOfWork.GetPlanRepository().GetIdAsync(Id, cancellationToken);
        if (plan == null) return Results.NotFound("Plan not found");
        var Plan = new PlanModelView
        {
            Name = plan.Name,
            Description = plan.Description,
            DurationDays = plan.DurationInDays,
            Price = plan.Price,
            IsActive = plan.IsActive,
        };
        return Results<PlanModelView>.Success(Plan);

    }

    public async Task<Results<UpdatePlanModelView>> GetPlanToUpdate(int id, CancellationToken cancellationToken)
    {
        var plan = await unitOfWork.GetPlanRepository().GetIdAsync(id, cancellationToken);

        var memberShip = await HasMemberShip(id, cancellationToken);
        if (plan is null && await HasMemberShip(id, cancellationToken)) return Results.BadRequest("Plan Still Has Memberships");

        return Results<UpdatePlanModelView>.Success(new UpdatePlanModelView()
        {
            Description = plan.Description,
            PlanName = plan.Name,
            Price = plan.Price,
            DurationDays = plan.DurationInDays

        });
    }

    private async Task<bool> HasMemberShip(int PlanId, CancellationToken cancellationToken)
    {
        var memperships = await unitOfWork.GetRepository<MemberShip>().ListAsync(Predicate: m => m.PlanId == PlanId && m.Status == "Active", cancellationToken: cancellationToken);

        return memperships.Any();

    }

    public async Task<Results<bool>> UpdatePlan(int id, PlanModelView planModelView, CancellationToken cancellationToken)
    {
        var plan = await unitOfWork.GetPlanRepository().GetIdAsync(id: id, cancellationToken: cancellationToken);
        if (plan is null || await HasMemberShip(id, cancellationToken)) return Results.NotFound("");
        (plan.Description, plan.Price, plan.DurationInDays, plan.UpdatedAt) = (planModelView.Description, planModelView.Price, planModelView.DurationDays, DateTime.UtcNow);

        unitOfWork.GetPlanRepository().Update(plan);
        var save = await unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken) > 0;
        return Results<bool>.Success(save);
    }
}

