using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace PulseFit.DAL.Repositories.Interfaces
{
    public interface IGenaricRepository<TEntity> where TEntity : class, new()
    {
        public Task<TEntity?> GetByIdAsync(int id);
        public Task<IEnumerable<TEntity>> ListAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>> include, Expression<Func<TEntity, bool>>? Predicate, Expression<Func<TEntity, Object>>? orderBy, Enums.OrderBy? orderByDirection = Enums.OrderBy.Ascending, bool AsNoTracking = true);

        public Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>>? Predicate, Expression<Func<TEntity, Object>>? orderBy, Enums.OrderBy? orderByDirection = Enums.OrderBy.Ascending, bool AsNoTracking = true);
        public Task AddAsync(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);

        public Task<int> SaveChangesAsync();
    }
}
