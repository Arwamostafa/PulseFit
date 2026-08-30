using PulseFit.DAL.QueryServices.Dtos;

namespace PulseFit.DAL.QueryServices;

public interface ISessionQueryService
{
    public Task<IReadOnlyList<SessionInsexQueryServiceDTO>> Index(CancellationToken cancellationToken);
}

