using Sample.Domain.Models;

namespace Sample.Domain.Interfaces.Repositories;

public interface IAdoRepository<T> where T : Entity
{
    Task CreateAsync(T entity);
    Task<List<T>> ReadAllAsync();
    Task<T> FindByIdAsync(string Property, Guid id);
    Task UpdateAsync(T entity, string property, Guid id);
    Task DeleteAsync(string property, Guid id);
}
