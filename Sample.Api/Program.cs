using Sample.Api.Configuration.CQRS.Commands;
using Sample.Api.Configuration.GenericInjections;
using Sample.Api.Configuration.ORM;
using Sample.Api.Middlewares;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
var logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "logs");
if (!Directory.Exists(logDirectory))
{
	Directory.CreateDirectory(logDirectory);
}

var logFilePath = Path.Combine(logDirectory, "application.log");

Log.Logger = new LoggerConfiguration()
	.MinimumLevel.Information()  
	.MinimumLevel.Override("Microsoft", LogEventLevel.Warning) 
	.MinimumLevel.Override("System", LogEventLevel.Warning)     
	.WriteTo.Console()
	.WriteTo.File(logFilePath, rollingInterval: RollingInterval.Infinite,
		retainedFileCountLimit: null,
		fileSizeLimitBytes: null,
		buffered: false, 
		flushToDiskInterval: TimeSpan.FromSeconds(1)) 
	.CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.SqlConfiguration(builder.Configuration);
builder.Services.AddRepositoryDependency();
builder.Services.AddUsersDependency();
builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Add custom middleware
app.UseMiddleware<ExceptionMiddleware>();
//app.UseMiddleware<AuthenticatorMiddleware>();

app.Run();
