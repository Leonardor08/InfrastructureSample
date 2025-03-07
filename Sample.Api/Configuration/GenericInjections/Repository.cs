using Oracle.EntityFrameworkCore.Storage.Internal;
using Sample.Api.Configuration.ORM;
using Sample.Api.Middlewares;
using Sample.Domain.Interfaces;
using Sample.Domain.Interfaces.Repositories;
using Sample.Domain.Resources.Constants;
using Sample.Infraestructure.Data.AdoDbContext;
using Sample.Infraestructure.Repository;
using System.Configuration;

namespace Sample.Api.Configuration.GenericInjections;

public static class Repository
{
    public static IServiceCollection AddRepositoryDependency(this IServiceCollection services)
    {
        services.AddScoped<OracleDataContext>();
        services.AddScoped(typeof(ITransactionScope), typeof(OracleDataContext));
        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
		services.AddScoped(typeof(IAdoRepository<>), typeof(GenericAdoRepository<>));
		return services;
    }
}
