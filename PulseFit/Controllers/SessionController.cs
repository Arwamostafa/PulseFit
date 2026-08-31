using PulseFit.BLL.ModelViews;
using PulseFit.BLL.ModelViews.Enums;
using PulseFit.PL.Extensions;

namespace PulseFit.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly ILogger<SessionController> _logger;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public async Task<IActionResult> Index(CancellationToken ct)
        {
            return this.ViewIndex<IReadOnlyList<SessionViewModel>>(await _sessionService.GetAllSessions(ct), "error");
            //var sessions = await _sessionService.GetAllSessions(ct);

            //return View(sessions);
        }

        public async Task<IActionResult> GetDetailedSession(int id, CancellationToken ct)
        => this.ViewDetails<SessionDetailsViewModel>(await _sessionService.GetDetaildSession(id, ct), "error", nameof(Index));


    }
}
