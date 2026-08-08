using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PulseFit.DAL.Entities;
using PulseFit.DAL.Enums;
using PulseFit.DAL.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PulseFit.DAL.Repositories.Classes
{
    public class GenaricRepository<TEntity>(PluseFitDbContext pluseFitDbContext) : IGenaricRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly PluseFitDbContext _pluseFitDbContext = pluseFitDbContext;

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default) => await _pluseFitDbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        public Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>>? Predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null, bool AsNoTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _pluseFitDbContext.Set<TEntity>();
            if (AsNoTracking)
                query = query.AsNoTracking();
            else
                query = query.AsTracking();
            if (Predicate != null)
                query = query.Where(Predicate);
            if (include != null)
                query = include(query);
            return query.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
        public async Task<IEnumerable<TEntity>> ListAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null, Expression<Func<TEntity, bool>>? Predicate = null, Expression<Func<TEntity, object>>? orderBy = null, OrderBy? orderByDirection = Enums.OrderBy.Ascending, bool AsNoTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _pluseFitDbContext.Set<TEntity>();

            if (AsNoTracking)
                query = query.AsNoTracking();
            else
                query = query.AsTracking();

            if (Predicate != null)
                query = query.Where(Predicate);


            if (include != null)
                query = include(query);

            if (orderBy != null)
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);

            return await query.ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var result = await _pluseFitDbContext.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken);
            return result;
        }
        public void Update(TEntity entity) => _pluseFitDbContext.Update(entity);
        public void Delete(TEntity entity) => _pluseFitDbContext.Remove(entity);

        public async Task<TEntity?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _pluseFitDbContext.Set<TEntity>().FindAsync(id, cancellationToken);
            return entity;
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _pluseFitDbContext.SaveChangesAsync(cancellationToken);

        public async Task<TEntity?> FindByIdWithDeletedEntityAsync(int id, CancellationToken cancellationToken = default)
        => await _pluseFitDbContext.Set<TEntity>().IgnoreQueryFilters().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        public void SoftDelete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;
            _pluseFitDbContext.Update(entity);
        }
    }
}
