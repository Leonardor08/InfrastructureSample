using Sample.Api.Configuration.CQRS.Commands;
using Sample.Api.Configuration.GenericInjections;
using Sample.Api.Configuration.ORM;
using Sample.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.SqlConfiguration(builder.Configuration);
builder.Services.AddRepositoryDependency();
builder.Services.AddUsersDependency();
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

app.Run();
