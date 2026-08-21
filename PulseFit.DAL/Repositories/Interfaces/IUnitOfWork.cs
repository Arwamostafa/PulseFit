
using PulseFit.DAL.Entities;

namespace PulseFit.DAL.Repositories.Interfaces;

public interface IUnitOfWork
{

    public IGenaricRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new();
    public IPlanRepository GetPlanRepository();
    public IMemberRepository GetMemberRepository();

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    public Task RollbackAsync(CancellationToken cancellationToken);
    public Task CommitAsync(CancellationToken cancellationToken);

    public Task BeginTrasaction(CancellationToken cancellationToken);

}

