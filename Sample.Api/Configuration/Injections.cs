using Sample.Api.Configuration.CQRS.Commands;
using Sample.Api.Configuration.GenericInjections;
using Sample.Api.Configuration.ORM;

namespace Sample.Api.Configuration
{
	public static class Injections
	{
		public static void Inject(this WebApplicationBuilder webApplicationBuilder)
		{
			webApplicationBuilder.Services.SqlConfiguration(webApplicationBuilder.Configuration);
			webApplicationBuilder.Services.AddRepositoryDependency();
			webApplicationBuilder.Services.AddUsersDependency();
			webApplicationBuilder.Services.AddControllers();
			webApplicationBuilder.Services.AddEndpointsApiExplorer();
			webApplicationBuilder.Services.AddSwaggerGen();
		}
	}
}
