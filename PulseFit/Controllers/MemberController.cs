using Microsoft.AspNetCore.Mvc;
using PulseFit.BLL.ModelViews;
using PulseFit.DAL;

namespace PulseFit.PL.Controllers
{
    public class MemberController(IMemberService  memberService) : Controller
    {
        private readonly IMemberService _memberService = memberService;

        public async Task<IActionResult> GetAllMembers(CancellationToken cancellationToken)
        {
            var members = await  _memberService.ListMembersAsync(cancellationToken);
        
            return View(members);
        }
    }
}
