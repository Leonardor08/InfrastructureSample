using Microsoft.EntityFrameworkCore;
using Sample.Domain.Resources.Constants;
using Sample.Infraestructure.Data.AdoDbContext;
using Sample.Infraestructure.Data.EFDbContext;
using Oracle.ManagedDataAccess.Client; // Importa Oracle ADO.NET

namespace Sample.Api.Configuration.ORM
{
	public static class OracleDbConfiguration
	{
		public static IServiceCollection OracleConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<OracleDataContext>(provider =>
			{
				var connection = new OracleConnection(configuration.GetConnectionString(DomainConstants.ORACLE_CONNECTION));
				return new OracleDataContext(connection, null);
			});
			return services;
		}
	}
}
