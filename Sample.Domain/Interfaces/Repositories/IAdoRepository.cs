using Sample.Domain.Models;

namespace Sample.Domain.Interfaces.Repositories;

public interface IAdoRepository<T> where T : Entity
{
    Task CreateAsync(T entity);
    List<T> ReadAllAsync();
    T FindByIdAsync(string Property, Guid id);
    Task UpdateAsync(T entity, string property, Guid id);
    Task DeleteAsync(string property, Guid id);
}
