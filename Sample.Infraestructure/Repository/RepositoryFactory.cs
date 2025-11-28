using Microsoft.Extensions.DependencyInjection;
using Sample.Application.Interfaces.Factories;
using Sample.Application.Interfaces.Repositories;
using Sample.Domain.Models;
using Sample.Infraestructure.Data.EFDbContext;

namespace Sample.Infraestructure.Repository;

public class RepositoryFactory<T, TKey>(IServiceScopeFactory scope) 
	: IRepositoryFactory<T, TKey>
	where T : class, IEntity<TKey>, new()
	where TKey : notnull
{
	private readonly IServiceScopeFactory _scopeFactory = scope;

	ISqlRepository<T, TKey> IRepositoryFactory<T, TKey>.CreateRepository<T, TKey>()
	{
		var scope = _scopeFactory.CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
		return new SqlGenericRepository<T, TKey>(context);
	}
}
