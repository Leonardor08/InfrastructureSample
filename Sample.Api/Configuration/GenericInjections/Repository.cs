using Sample.Api.Middlewares;
using Sample.Domain.Interfaces;
using Sample.Infraestructure.Repository;

namespace Sample.Api.Configuration.GenericInjections;

public static class Repository
{
    public static IServiceCollection AddRepositoryDependency(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        return services;
    }
}
