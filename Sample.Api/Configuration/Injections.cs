using Sample.Api.Configuration.CQRS.Commands;
using Sample.Api.Configuration.CQRS.Queries;
using Sample.Api.Configuration.GenericInjections;
using Sample.Api.Configuration.ORM;
using System.Reflection;

namespace Sample.Api.Configuration
{
    public static class Injections
	{
		public static void Inject(this WebApplicationBuilder webApplicationBuilder)
		{
			webApplicationBuilder.Services.OracleConfiguration(webApplicationBuilder.Configuration);
			webApplicationBuilder.Services.AddRepositoryDependency();
            webApplicationBuilder.Services.AddMediatR(configuration 
				=> configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            webApplicationBuilder.Services.AddCommandsDependency();
			webApplicationBuilder.Services.AddQueriesDependency();
			webApplicationBuilder.Services.AddControllers();
			webApplicationBuilder.Services.AddEndpointsApiExplorer();
			webApplicationBuilder.Services.AddSwaggerGen();
		}
	}
}
