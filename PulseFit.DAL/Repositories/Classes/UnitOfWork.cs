using Microsoft.EntityFrameworkCore.Storage;
using PulseFit.DAL.Entities;
using PulseFit.DAL.Repositories.Interfaces;

namespace PulseFit.DAL.Repositories.Classes;

public class UnitOfWork(PluseFitDbContext dbContext) : IUnitOfWork
{
    private readonly Dictionary<Type, object> repositories = new();
    IDbContextTransaction transaction;

    public async Task BeginTrasaction(CancellationToken cancellationToken) => transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);


    public IGenaricRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
    {
        var type = typeof(TEntity);
        if (repositories.ContainsKey(type))
        {
            return (IGenaricRepository<TEntity>)repositories[type];
        }
        var repository = new GenaricRepository<TEntity>(dbContext);
        repositories.Add(type, repository);
        return repository;
    }
    public IPlanRepository GetPlanRepository() => new PlanRepository(dbContext);

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        if (transaction == null)
        {
            throw new InvalidOperationException("No transaction in progress.");
        }
        await transaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken)
    {
        if (transaction == null)
        {
            throw new InvalidOperationException("No transaction in progress.");
        }
        await transaction.RollbackAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) => await dbContext.SaveChangesAsync(cancellationToken);

}

