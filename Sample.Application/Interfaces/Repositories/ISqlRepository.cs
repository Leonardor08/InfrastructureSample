using System.Linq.Expressions;

namespace Sample.Application.Interfaces.Repositories;

public interface ISqlRepository<T, TKey> where TKey : notnull
{
	Task SaveAsync();
    IQueryable<T> GetQueryablel();
    Task CreateAsync(T entity);
    Task DeleteAsync(TKey entity);
    Task<T> FindByIdAsync(TKey id);
    Task<List<T>> GetAllAsync();
    Task<T> GetByFilter(Expression<Func<T, bool>> filter);
    IEnumerable<T> GetByFilterOrdered(Expression<Func<T, bool>> predicate, 
        Expression<Func<T, object>> orderBy, bool? isDesc = true);
    Task UpdateAsync(T entity);
}
