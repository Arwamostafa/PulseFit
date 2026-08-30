using PulseFit.BLL.ModelViews;

namespace PulseFit.BLL.Services.Contracts;

public interface ISessionService
{
    public Task<IReadOnlyList<SessionViewModel>> GetAllSessions(CancellationToken ct);
}

