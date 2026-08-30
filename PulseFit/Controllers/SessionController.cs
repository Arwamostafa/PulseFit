using Microsoft.AspNetCore.Mvc;
using PulseFit.BLL.Services.Contracts;

namespace PulseFit.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly ILogger<SessionController> _logger;

        public SessionController(ISessionService sessionService, ILogger<SessionController> logger)
        {
            _sessionService = sessionService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var sessions = await _sessionService.GetAllSessions(ct);
            _logger.LogInformation("Fetched sessions successfully");
            return View(sessions);
        }
    }
}
