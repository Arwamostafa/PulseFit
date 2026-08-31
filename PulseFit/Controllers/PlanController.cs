using Mapster;
using PulseFit.BLL.ModelViews;
using PulseFit.PL.Extensions;

namespace PulseFit.PL.Controllers
{
    public class PlanController(IPlanService planService, ILogger<PlanController> logger) : Controller
    {
        private readonly IPlanService planService = planService;
        private readonly ILogger<PlanController> _logger = logger;

        public async Task<IActionResult> GetAllPlans(CancellationToken cancellationToken)
            => this.ViewIndex<IReadOnlyList<PlanModelView>>(await planService.GetAllPlans(cancellationToken), "error");


        public async Task<IActionResult> GetPlan(int id, CancellationToken cancellationToken)
           => this.ViewDetails<PlanModelView>(await planService.GetPlanById(id, cancellationToken), "error", nameof(GetAllPlans));



        [HttpGet]
        public async Task<IActionResult> GetPlanToUpdate(int id, CancellationToken cancellationToken)
          => this.ViewDetails<PlanToUpdateViewModel>(await planService.GetPlanToUpdate(id, cancellationToken), "error", nameof(GetAllPlans));


        [HttpPost]
        public async Task<IActionResult> UpdatePlan(int id, PlanToUpdateViewModel planModelView, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                var viewData = planModelView.Adapt<PlanToUpdateViewModel>();
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
