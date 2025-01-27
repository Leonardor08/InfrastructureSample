using Microsoft.EntityFrameworkCore;
using Sample.Domain.Models;

namespace Sample.Infraestructure.Data.EFDbContext;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}
