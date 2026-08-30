using Microsoft.AspNetCore.Mvc.Rendering;
using PulseFit.BLL.ModelViews;
using PulseFit.DAL.Enums;

namespace PulseFit.PL.Controllers
{
    public class MemberController(IMemberService memberService, ILogger<MemberController> logger) : Controller
    {
        private readonly IMemberService _memberService = memberService;
        private readonly ILogger<MemberController> _logger = logger;

        public async Task<IActionResult> GetAllMembers(CancellationToken cancellationToken)
        {
            var result = await _memberService.ListMembersAsync(cancellationToken);
            if (!result.IsSuccess)
            {
                _logger.LogWarning("Failed to fetch all members: {Error}", result.Error);
                TempData["ErrorMessage"] = result.Error;
                return RedirectToAction(nameof(GetAllMembers));
            }

            return View(result.Data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.BloodTypes = Enum.GetValues(typeof(BloodType)).Cast<BloodType>().Select(b => new SelectListItem
            {
                Text = b.ToString(),
                Value = b.ToString()
            });
            return View(new CreateMemberViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMemberViewModel member, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.BloodTypes = Enum.GetValues(typeof(BloodType)).Cast<BloodType>().Select(b => new SelectListItem
                {
                    Text = b.ToString(),
                    Value = b.ToString()
                });
                return View(member);
            }

            var result = await _memberService.CreateMemberAsync(member, cancellationToken);
            if (!result.IsSuccess)
            {
                _logger.LogWarning("Failed to create member: {Error}", result.Error);
                ModelState.AddModelError(string.Empty, result.Error);
                TempData["ErrorMessage"] = "Error happened while adding member!";
                ViewBag.BloodTypes = Enum.GetValues(typeof(BloodType)).Cast<BloodType>().Select(b => new SelectListItem
                {
                    Text = b.ToString(),
                    Value = b.ToString()
                });
                return View(member);
            }

            _logger.LogInformation("Member created successfully.");
            TempData["SuccessMessage"] = "Member added successfully.";
            return RedirectToAction(nameof(GetAllMembers));
        }


        [HttpGet]
        public async Task<IActionResult> GetMemberDetails(int id, CancellationToken cancellationToken)
        {
            var result = await _memberService.GetByIdAsync(id, cancellationToken);
            if (!result.IsSuccess)
            {
                _logger.LogWarning("Failed to get member with ID {Id}: {Error}", id, result.Error);
                TempData["ErrorMessage"] = result.Error;
                return RedirectToAction(nameof(GetAllMembers));
            }
            return View(result.Data);
        }

        public async Task<IActionResult> GetHealthRecord(int id, CancellationToken cancellationToken)
        {
            var result = await _memberService.GetHealthRecordAsync(id, cancellationToken);
            if (!result.IsSuccess)
            {
                _logger.LogWarning("Failed to get health record for member ID {Id}: {Error}", id, result.Error);
                TempData["ErrorMessage"] = result.Error;
                return RedirectToAction(nameof(GetAllMembers));
            }
            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetMemberToUpdate(int id, CancellationToken cancellationToken)
        {
            var result = await _memberService.GetMemberToUpdateAsync(id, cancellationToken);
            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Error;
                return RedirectToAction(nameof(GetAllMembers));
            }
            return View(result.Data);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateMember(int id, MemberToUpdateViewModel member, CancellationToken cancellationToken)
        {
            var result = await _memberService.UpdateMemberAsync(id, member, cancellationToken);
            if (!result.IsSuccess)
            {
                _logger.LogWarning("Failed to update member ID {Id}: {Error}", id, result.Error);
                ModelState.AddModelError(string.Empty, result.Error);
                TempData["ErrorMessage"] = "Error happened while updating member!";
                return View("GetMemberToUpdate", member);
            }

            _logger.LogInformation("Member ID {Id} updated successfully.", id);
            TempData["SuccessMessage"] = "Member updated successfully.";
            return RedirectToAction(nameof(GetAllMembers));
        }
        [HttpGet]
        public async Task<IActionResult> DeleteMemberConfirmed(int id, CancellationToken cancellationToken)
        {
            var result = await _memberService.GetByIdAsync(id, cancellationToken);
            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Error;
                return RedirectToAction(nameof(GetAllMembers));
            }

            return View(result?.Data?.Id);

        }
        [HttpPost]
        public async Task<IActionResult> DeleteMember(int id, CancellationToken cancellationToken)
        {
            var result = await _memberService.DeleteMemberAsync(id, cancellationToken);
            if (!result.IsSuccess)
            {
                _logger.LogWarning("Failed to delete member ID {Id}: {Error}", id, result.Error);
                TempData["ErrorMessage"] = "Error happened while deleting member!";
                return RedirectToAction(nameof(GetAllMembers));
            }
            _logger.LogInformation("Member ID {Id} deleted successfully.", id);
            TempData["SuccessMessage"] = "Member deleted successfully.";
            return RedirectToAction(nameof(GetAllMembers));
        }
    }
}
