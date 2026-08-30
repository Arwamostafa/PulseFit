using Microsoft.AspNetCore.Mvc;
using PulseFit.BLL.Services.Contracts;

namespace PulseFit.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var sessions = await _sessionService.GetAllSessions(ct);
            return View(sessions);
        }
    }
}
