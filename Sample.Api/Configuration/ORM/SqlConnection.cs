using Microsoft.EntityFrameworkCore;
using Sample.Domain.Resources.Constants;
using Sample.Infraestructure.Data.EFDbContext;

namespace Sample.Api.Configuration.ORM;

public static class SqlConnection
{
    public static IServiceCollection SqlConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString(DomainConstants.SQL_CONNECTION)));
        return services;
    }
}
