using PulseFit.BLL.ModelViews;

namespace PulseFit.PL.Controllers
{
    public class PlanController(IPlanService planService) : Controller
    {
        private readonly IPlanService planService = planService;

        public async Task<IActionResult> GetAllPlans(CancellationToken cancellationToken)
        {
            var plans = await planService.GetAllPlans(cancellationToken);

            if (!plans.IsSuccess)
            {
                TempData["ErrorMessage"] = plans.Error;
                return RedirectToAction(nameof(GetAllPlans));
            }

            var PlanViews = plans.Data!.Select(p => new PlanModelView
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                DurationDays = p.DurationDays,
                Price = p.Price,
                IsActive = p.IsActive,
            });

            return View(PlanViews);
        }

        public async Task<IActionResult> GetPlan(int id, CancellationToken cancellationToken)
        {
            var plan = await planService.GetPlanById(id, cancellationToken);
            if (!plan.IsSuccess)
            {
                TempData["ErrorMessage"] = plan.Error;
                return RedirectToAction(nameof(GetAllPlans));
            }

            var PlanView = new PlanModelView
            {
                Name = plan.Data!.Name,
                Description = plan.Data.Description,
                DurationDays = plan.Data.DurationDays,
                Price = plan.Data.Price,
                IsActive = plan.Data.IsActive,
            };
            return View(PlanView);

        }
    }
}
