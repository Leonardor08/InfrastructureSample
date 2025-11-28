using Sample.Application.Interfaces.Repositories;
using Sample.Domain.Models;

namespace Sample.Application.Interfaces.Factories;

public interface IRepositoryFactory<T, TKey> where TKey : notnull
{
	ISqlRepository<T, TKey> CreateRepository<T, TKey>() 
		where T : class, IEntity<TKey>, new()
		where TKey : notnull;
}
