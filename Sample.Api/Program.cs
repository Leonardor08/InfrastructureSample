using Sample.Infraestructure.Configuration.GenericInjections;
using Sample.Infraestructure.Configuration.ORM;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.SqlConfiguration(builder.Configuration);
builder.Services.AddRepositoryDependency();
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

app.Run();
