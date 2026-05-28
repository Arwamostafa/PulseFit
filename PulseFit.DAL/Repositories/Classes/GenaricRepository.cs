using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PulseFit.DAL.Entities;
using PulseFit.DAL.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PulseFit.DAL.Repositories.Classes
{
    public class GenaricRepository<TEntity>(PluseFitDbContext pluseFitDbContext) : IGenaricRepository<TEntity> where TEntity : BaseEntity, new()
    {
        public async Task AddAsync(TEntity entity) => await pluseFitDbContext.Set<TEntity>().AddAsync(entity);




        public async Task<TEntity?> GetByIdAsync(int id) => await pluseFitDbContext.Set<TEntity>().FindAsync(id);



        public async Task<IEnumerable<TEntity>> ListAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>> include, Expression<Func<TEntity, bool>>? Predicate, Expression<Func<TEntity, object>>? orderBy, Enums.OrderBy? orderByDirection = Enums.OrderBy.Ascending, bool AsNoTracking = true)
        {
            IQueryable<TEntity> query = pluseFitDbContext.Set<TEntity>();

            if (AsNoTracking)
                query = query.AsNoTracking();
            else
                query = query.AsTracking();

            if (Predicate != null)
                query = query.Where(Predicate);

            if (orderBy != null)
                if (orderByDirection == Enums.OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);

            if (include != null)
                query = include(query);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>>? Predicate, Expression<Func<TEntity, object>>? orderBy, Enums.OrderBy? orderByDirection = Enums.OrderBy.Ascending, bool AsNoTracking = true)
        {
            IQueryable<TEntity> query = pluseFitDbContext.Set<TEntity>();

            if (AsNoTracking)
                query = query.AsNoTracking();
            else
                query = query.AsTracking();

            if (Predicate != null)
                query = query.Where(Predicate);
            if (orderBy != null)
                if (orderByDirection == Enums.OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            return await query.ToListAsync();

        }


        public void Update(TEntity entity) => pluseFitDbContext.Update(entity);
        public void Delete(TEntity entity) => pluseFitDbContext.Remove(entity);
        public async Task<int> SaveChangesAsync() => await pluseFitDbContext.SaveChangesAsync();



    }
}
