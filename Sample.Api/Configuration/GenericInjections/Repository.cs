using MediatR;
using Sample.Application.Behaviors;
using Sample.Application.Interfaces;
using Sample.Application.Interfaces.Repositories;
using Sample.Infraestructure.Data.AdoDbContext;
using Sample.Infraestructure.Repository;

namespace Sample.Api.Configuration.GenericInjections;

public static class Repository
{
    public static IServiceCollection AddRepositoryDependency(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
        services.AddScoped(typeof(IRepository<,>), typeof(GenericRepository<,>));
        services.AddScoped(typeof(ITransactionScope), typeof(OracleDataContext));

        return services;
    }
}
