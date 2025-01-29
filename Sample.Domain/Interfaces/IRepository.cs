using Sample.Domain.Models;
using System.Linq.Expressions;

namespace Sample.Domain.Interfaces;

public interface IRepository<T> where T : Entity
{
    Task<T> CreateAsync(T entity);
    Task<List<T>> ReadAllAsync();
    Task<T> FindByIdAsync(Guid id);
    Task<T> GetByFilter(Expression<Func<T, bool>> filter);
    IEnumerable<T> GetByFilterOrdered(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy, bool? isDesc = true);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
