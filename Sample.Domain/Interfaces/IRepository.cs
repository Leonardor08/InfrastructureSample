using Sample.Domain.Models;

namespace Sample.Domain.Interfaces;

public interface IRepository<T> where T : Entity
{
    Task<T> CreateAsync(T entity);
    Task<List<T>> ReadAllAsync();
    Task<T> FindByIdAsync(Guid id);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
