using MediatR;
using Sample.Application.Behaviors;
using Sample.Application.Interfaces.Factories;
using Sample.Application.Interfaces.Repositories;
using Sample.Infraestructure.Repository;

namespace Sample.Api.Configuration.GenericInjections;

public static class Repository
{
	public static IServiceCollection AddRepositoryDependency(this IServiceCollection services)
    {
        services.AddSingleton(typeof(IRepositoryFactory<,>), typeof(RepositoryFactory<,>));
        services.AddTransient(typeof(ISqlRepository<,>), typeof(SqlGenericRepository<,>));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        return services;
    }
}
