using PulseFit.BLL.Models;
using PulseFit.BLL.ModelViews;

namespace PulseFit.BLL.Services.Contracts;

public interface IPlanService
{
    public Task<Results<IEnumerable<PlanModelView>>> GetAllPlans(CancellationToken cancellationToken);

    public Task<Results<PlanModelView>> GetPlanById(int Id, CancellationToken cancellationToken);

    public Task<Results<bool>> UpdatePlan(int id, PlanToUpdateViewModel planModelView, CancellationToken cancellationToken);

    public Task<Results<PlanToUpdateViewModel>> GetPlanToUpdate(int id, CancellationToken cancellationToken);

    public Task<Results<bool>> ToggleActive(int id, CancellationToken cancellationToken);


}

