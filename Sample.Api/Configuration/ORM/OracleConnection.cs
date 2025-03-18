using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using Sample.Domain.Resources.Constants;
using Sample.Infraestructure.Data.AdoDbContext;
using Sample.Infraestructure.Data.EFDbContext;
using Sample.Infraestructure.Repository;

namespace Sample.Api.Configuration.ORM
{
    public static class OracleDbConfiguration
	{
		public static IServiceCollection OracleConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<OracleConnection>(provider =>
			{
				var connection = new OracleConnection(configuration.GetConnectionString(DomainConstants.ORACLE_CONNECTION));
				connection.Open(); 
				return connection;
			});

			services.AddScoped<OracleDataContext>(provider =>
			{
				var connection = provider.GetRequiredService<OracleConnection>(); 
				return new OracleDataContext(connection);
			});

			return services;
		}

	}
}
