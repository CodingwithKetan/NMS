using System.Linq.Expressions;

namespace LiteNMS.Infrastructure;

public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
    Task AddRangeAsync(IEnumerable<T> entities);
}

