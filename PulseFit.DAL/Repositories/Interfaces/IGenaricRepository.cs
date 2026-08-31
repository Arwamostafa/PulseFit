using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace PulseFit.DAL.Repositories.Interfaces
{
    public interface IGenaricRepository<TEntity> where TEntity : class, new()
    {
        //public Task<TEntity?> GetByIdAsync(int id, Expression<Func<TEntity, bool>>? Predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null, bool AsNoTracking = true, CancellationToken cancellationToken = default);

        public Task<TEntity?> FindByIdAsync(int id, CancellationToken cancellationToken = default);

        public Task<TEntity?> FindByIdWithDeletedEntityAsync(int id, CancellationToken cancellationToken = default);
        public Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>>? Predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null, bool AsNoTracking = true, CancellationToken cancellationToken = default);

        public Task<IReadOnlyList<TEntity>> ListAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null, Expression<Func<TEntity, bool>>? Predicate = null, Expression<Func<TEntity, Object>>? orderBy = null, Enums.OrderBy? orderByDirection = Enums.OrderBy.Ascending, bool AsNoTracking = true, CancellationToken cancellationToken = default);

        //public Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>>? Predicate, Expression<Func<TEntity, Object>>? orderBy, Enums.OrderBy? orderByDirection = Enums.OrderBy.Ascending, bool? AsNoTracking = true, CancellationToken cancellationToken = default);
        public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        public void Update(TEntity entity);

        public void Delete(TEntity entity);
        public void SoftDelete(TEntity entity);

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
