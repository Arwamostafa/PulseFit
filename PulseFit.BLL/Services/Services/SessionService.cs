using Mapster;
using Microsoft.EntityFrameworkCore;
using PulseFit.BLL.Models;
using PulseFit.BLL.ModelViews;
using PulseFit.BLL.ModelViews.Enums;
using PulseFit.BLL.Services.Contracts;
using PulseFit.DAL.Entities;
using PulseFit.DAL.QueryServices;
using PulseFit.DAL.Repositories.Interfaces;

namespace PulseFit.BLL.Services.Services;

public class SessionService(ISessionQueryService sessionQueryService, IGenaricRepository<Session> sessionRep) : ISessionService
{
    private readonly IGenaricRepository<Session> _sessionRep = sessionRep;

    public async Task<Results<IReadOnlyList<SessionViewModel>>> GetAllSessions(CancellationToken ct)
    {
        var sessions = await sessionQueryService.Index(ct);
        return Results<IReadOnlyList<SessionViewModel>>.Success(sessions.Adapt<IReadOnlyList<SessionViewModel>>());
    }

    public async Task<Results<SessionDetailsViewModel>> GetDetaildSession(int id, CancellationToken ct)
    {
        var session = await _sessionRep.FindAsync(
            s => s.Id == id,
            s => s.Include(s => s.Trainer).Include(s => s.Bookings),
            cancellationToken: ct
            );
        if (session is null) return Results<SessionDetailsViewModel>.Failure("session not found", nameof(session));

        return Results<SessionDetailsViewModel>.Success(session.Adapt<SessionDetailsViewModel>());
    }
}

