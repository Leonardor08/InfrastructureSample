using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Sample.Domain.Resources.Constants;
using Sample.Infraestructure.Data.AdoDbContext;

namespace Sample.Api.Configuration.ORM
{
    public static class OracleDbConfiguration
	{
		public static IServiceCollection OracleConfiguration(this IServiceCollection services, IConfiguration configuration)
		{

            services.AddScoped<OracleConnection>(provider =>
            {
                var connection = configuration.GetConnectionString(DomainConstants.ORACLE_CONNECTION);
                return new OracleConnection(connection);
            });

            services.AddScoped<OracleDataContext>(provider =>
			{
				var connection = new OracleConnection(configuration.GetConnectionString(DomainConstants.ORACLE_CONNECTION));
				return new OracleDataContext(connection);
			});

            return services;
		}
	}
}
