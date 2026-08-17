using PulseFit.BLL.ModelViews;

namespace PulseFit.PL.Controllers
{
    public class MemberController(IMemberService memberService) : Controller
    {
        private readonly IMemberService _memberService = memberService;

        public async Task<IActionResult> GetAllMembers(CancellationToken cancellationToken)
        {
            var result = await _memberService.ListMembersAsync(cancellationToken);
            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.Error);

            return View(result.Data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateMemberViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMemberViewModel member, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(member);

            var result = await _memberService.CreateMemberAsync(member, cancellationToken);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Error);

                return View(member);
            }

            TempData["SuccessMessage"] = "Member added successfully.";
            return RedirectToAction(nameof(GetAllMembers));
        }
    }
}
