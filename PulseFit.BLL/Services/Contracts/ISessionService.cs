using PulseFit.BLL.Models;
using PulseFit.BLL.ModelViews;
using PulseFit.BLL.ModelViews.Enums;

namespace PulseFit.BLL.Services.Contracts;

public interface ISessionService
{
    public Task<Results<IReadOnlyList<SessionViewModel>>> GetAllSessions(CancellationToken ct);
    public Task<Results<SessionDetailsViewModel>> GetDetaildSession(int id, CancellationToken ct);
}

