using Mapster;
using PulseFit.BLL.ModelViews;

namespace PulseFit.PL.Controllers
{
    public class PlanController(IPlanService planService, ILogger<PlanController> logger) : Controller
    {
        private readonly IPlanService planService = planService;
        private readonly ILogger<PlanController> _logger = logger;

        public async Task<IActionResult> GetAllPlans(CancellationToken cancellationToken)
        {
            var plans = await planService.GetAllPlans(cancellationToken);

            if (!plans.IsSuccess)
            {
                _logger.LogWarning("Failed to fetch all plans: {Error}", plans.Error);
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
                _logger.LogWarning("Failed to fetch plan ID {Id}: {Error}", id, plan.Error);
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

        [HttpGet]
        public async Task<IActionResult> GetPlanToUpdate(int id, CancellationToken cancellationToken)
        {
            var plan = await planService.GetPlanToUpdate(id, cancellationToken);
            if (!plan.IsSuccess)
            {
                _logger.LogWarning("Failed to fetch plan ID {Id} for update: {Error}", id, plan.Error);
                TempData["ErrorMessage"] = plan.Error;
                return RedirectToAction(nameof(GetAllPlans));
            }

            return View(plan.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePlan(int id, PlanToUpdateViewModel planModelView, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                var viewData = planModelView.Adapt<PlanToUpdateViewModel>();
                viewData.Id = id;
                return View("GetPlanToUpdate", viewData);
            }

            var result = await planService.UpdatePlan(id, planModelView, cancellationToken);
            if (!result.IsSuccess)
            {
                _logger.LogWarning("Failed to update plan ID {Id}: {Error}", id, result.Error);
                TempData["ErrorMessage"] = result.Error;
                ModelState.AddModelError(string.Empty, result.Error);
                var viewData = planModelView.Adapt<PlanToUpdateViewModel>();
                viewData.Id = id;
                return View("GetPlanToUpdate", viewData);
            }

            _logger.LogInformation("Plan ID {Id} updated successfully.", id);
            TempData["SuccessMessage"] = "Plan updated successfully";
            return RedirectToAction(nameof(GetAllPlans));
        }

        [HttpPost]
        public async Task<IActionResult> ToggleActive(int id, CancellationToken cancellationToken)
        {
            var result = await planService.ToggleActive(id, cancellationToken);
            if (!result.IsSuccess)
            {
                _logger.LogWarning("Failed to toggle active status for plan ID {Id}: {Error}", id, result.Error);
                TempData["ErrorMessage"] = result.Error;
            }
            else
            {
                _logger.LogInformation("Plan ID {Id} active status toggled successfully.", id);
                TempData["SuccessMessage"] = "Plan status updated successfully";
            }
            return RedirectToAction(nameof(GetAllPlans));
        }
    }
}
