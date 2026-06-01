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
        public async Task AddAsync(TEntity entity) => await pluseFitDbContext.Set<TEntity>().AddAsync(entity);


        public async Task<TEntity?> GetByIdAsync(int id, Expression<Func<TEntity, bool>>? Predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null, bool AsNoTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = pluseFitDbContext.Set<TEntity>();

            if (query == null)
                return null;

            if (AsNoTracking)
                query = query.AsNoTracking();
            else
                query = query.AsTracking();

            if (include != null)
                query = include(query);

            return await query.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
        public Task<TEntity?> GetByIdAsync(Expression<Func<TEntity, bool>>? Predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>? include = null, bool AsNoTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = pluseFitDbContext.Set<TEntity>();
            if (query == null)
                return Task.FromResult<TEntity?>(null);
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
            IQueryable<TEntity> query = pluseFitDbContext.Set<TEntity>();

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

        //public async Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>>? Predicate = null, Expression<Func<TEntity, object>>? orderBy = null, Enums.OrderBy? orderByDirection = OrderBy.Ascending, bool? AsNoTracking = true)
        //{
        //    IQueryable<TEntity> query = pluseFitDbContext.Set<TEntity>();

        //    if (AsNoTracking == true)
        //        query = query.AsNoTracking();
        //    else
        //        query = query.AsTracking();

        //    if (Predicate != null)
        //        query = query.Where(Predicate);

        //    if (orderBy != null)
        //        if (orderByDirection == OrderBy.Ascending)
        //            query = query.OrderBy(orderBy);
        //        else
        //            query = query.OrderByDescending(orderBy);

        //    return await query.ToListAsync();

        //}

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var result = await pluseFitDbContext.Set<TEntity>().AsNoTracking().ToListAsync();
            return result;
        }
        public void Update(TEntity entity) => pluseFitDbContext.Update(entity);
        public void Delete(TEntity entity) => pluseFitDbContext.Remove(entity);
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await pluseFitDbContext.SaveChangesAsync(cancellationToken);


    }
}
