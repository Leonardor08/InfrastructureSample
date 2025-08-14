using Sample.Domain.Models;

namespace Sample.Application.Interfaces.Repositories;

public interface IBatchRepository<T>
{
    Task SaveBatchAsync(IEnumerable<T> entities);
    Task UseAdoBatchAsync(List<Users> entities);
}