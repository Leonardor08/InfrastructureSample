using Sample.Api.Configuration;
using Sample.Api.Middlewares;
using Serilog;
using Serilog.Events;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
		var corsPolicyName = "AllowAngularFrontend";
		builder.Services.AddCors(options =>
		{
			options.AddPolicy(corsPolicyName, policy =>
			{
				policy.WithOrigins("http://localhost:4200") 
					  .AllowAnyHeader()
					  .AllowAnyMethod()
					  .AllowCredentials();
			});
		});


		//log configuration
		Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .WriteTo.Console()
            .WriteTo.File("logs/application.log", rollingInterval: RollingInterval.Infinite,
                retainedFileCountLimit: null,
                fileSizeLimitBytes: null,
                buffered: false,
                flushToDiskInterval: TimeSpan.FromSeconds(1))
            .CreateLogger();

        builder.Host.UseSerilog();

        builder.Inject();

		builder.Services.AddControllers();
		var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

		app.UseCors(corsPolicyName);
		app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        // Add custom middleware
        app.UseMiddleware<ExceptionMiddleware>();
        //app.UseMiddleware<AuthenticatorMiddleware>();

        app.Run();
    }
}