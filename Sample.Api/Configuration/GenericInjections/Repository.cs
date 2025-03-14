using Sample.Application.Interfaces.Repositories;
using Sample.Domain.Interfaces;
using Sample.Infraestructure.Data.AdoDbContext;
using Sample.Infraestructure.Repository;

namespace Sample.Api.Configuration.GenericInjections;

public static class Repository
{
    public static IServiceCollection AddRepositoryDependency(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<,>), typeof(GenericRepository<,>));
        services.AddScoped<ITransactionScope>(provider =>
        {
            var context = provider.GetRequiredService<OracleDataContext>();
            return (ITransactionScope)context;
        });
        return services;
    }
}
