using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sample.Infraestructure.Data.EFDbContext;

namespace Sample.Test.Infraestructure;

public class GenericWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Acá se configura los mocks de servios o bases de datos en memoria
            ServiceProvider serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDb");
                options.UseInternalServiceProvider(serviceProvider);
            });

            // Acá se elimina el contexto original y usa el de prueba
            ServiceProvider configurationServiceProvider = services.BuildServiceProvider();
            using IServiceScope scope = configurationServiceProvider.CreateScope();
            AppDbContext dataBaseContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dataBaseContext.Database.EnsureCreated();
            if (!dataBaseContext.Users.Any())
            {
                dataBaseContext.Users.Add(new() { Id = Guid.Parse("44197386-3893-4505-869d-04ea2187d293"), Name = "Oscar Test", Email = "Test@hotmail.com", Phone = "3212246801" });
                dataBaseContext.SaveChanges();
            }
        });    
    }
}
