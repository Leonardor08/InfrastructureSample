//using Microsoft.EntityFrameworkCore;
//using Oracle.ManagedDataAccess.Client;
//using Sample.Domain.Resources.Constants;
//using Sample.Infraestructure.Data.AdoDbContext;

//namespace Sample.Api.Configuration.ORM
//{
//    public static class OracleDbConfiguration
//	{
//		public static IServiceCollection OracleConfiguration(this IServiceCollection services, IConfiguration configuration)
//		{
//			services.AddScoped<OracleConnection>(provider =>
//			{
//				string connectionString = configuration.GetConnectionString(DomainConstants.ORACLE_CONNECTION)!;
//				OracleConnection connection = new(connectionString);
//				return connection;
//			});

//			services.AddScoped<OracleDataContext>(provider =>
//			{
//                OracleConnection oracleConnection = provider.GetRequiredService<OracleConnection>();
//				return new OracleDataContext(oracleConnection);
//			});

//            return services;
//		}

//	}
//}
