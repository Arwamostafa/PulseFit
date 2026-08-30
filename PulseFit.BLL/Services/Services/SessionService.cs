using Mapster;
using PulseFit.BLL.ModelViews;
using PulseFit.BLL.Services.Contracts;
using PulseFit.DAL.QueryServices;

namespace PulseFit.BLL.Services.Services;

public class SessionService(ISessionQueryService sessionQueryService) : ISessionService
{
    public async Task<IReadOnlyList<SessionViewModel>> GetAllSessions(CancellationToken ct)
    {
        var sessions = await sessionQueryService.Index(ct);
        return sessions.Adapt<IReadOnlyList<SessionViewModel>>();
    }
}

