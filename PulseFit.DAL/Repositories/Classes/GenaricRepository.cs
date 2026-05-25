using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PulseFit.DAL.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PulseFit.DAL.Repositories.Classes
{
    public class GenaricRepository<T>(PluseFitDbContext pluseFitDbContext) : IGenaricRepository<T> where T : class
    {
        public async Task AddAsync(T entity) => await pluseFitDbContext.Set<T>().AddAsync(entity);


        public void Delete(T entity) => pluseFitDbContext.Remove(entity);


        public async Task<T> GetByIdAsync(int id) => await pluseFitDbContext.Set<T>().FindAsync(id);



        public async Task<IEnumerable<T>> ListAsync(Func<IQueryable<T>, IIncludableQueryable<T, object?>> include, Expression<Func<T, bool>>? Predicate, Expression<Func<T, Object>>? orderBy, Enums.OrderBy? orderByDirection = Enums.OrderBy.Ascending)
        {
            IQueryable<T> query = pluseFitDbContext.Set<T>();

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

        public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>>? Predicate, Expression<Func<T, object>>? orderBy, Enums.OrderBy? orderByDirection = Enums.OrderBy.Ascending)
        {
            IQueryable<T> query = pluseFitDbContext.Set<T>();
            if (Predicate != null)
                query = query.Where(Predicate);
            if (orderBy != null)
                if (orderByDirection == Enums.OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            return await query.ToListAsync();

        }

        public async Task<int> SaveChangesAsync() => await pluseFitDbContext.SaveChangesAsync();


        public void Update(T entity) => pluseFitDbContext.Update(entity);

    }
}
