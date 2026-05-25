using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace PulseFit.DAL.Repositories.Interfaces
{
    public interface IGenaricRepository<T> where T : class
    {
        public Task<T> GetByIdAsync(int id);
        public Task<IEnumerable<T>> ListAsync(Func<IQueryable<T>, IIncludableQueryable<T, object?>> include, Expression<Func<T, bool>>? Predicate, Expression<Func<T, Object>>? orderBy, Enums.OrderBy? orderByDirection = Enums.OrderBy.Ascending);

        public Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>>? Predicate, Expression<Func<T, Object>>? orderBy, Enums.OrderBy? orderByDirection = Enums.OrderBy.Ascending);
        public Task AddAsync(T entity);
        public void Update(T entity);
        public void Delete(T entity);

        public Task<int> SaveChangesAsync();
    }
}
