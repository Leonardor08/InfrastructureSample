using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Domain.Constants;
using Sample.Infraestructure.Data.EFDbContext;

namespace Sample.Infraestructure.Configuration.ORM;

public static class SqlConnection
{
    public static IServiceCollection SqlConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(DomainConstants.SQL_CONNECTION)));
        return services;
    }
}
