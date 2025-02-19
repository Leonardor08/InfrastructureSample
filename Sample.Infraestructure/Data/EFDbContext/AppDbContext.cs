﻿using Microsoft.EntityFrameworkCore;
using Sample.Domain.Models;

namespace Sample.Infraestructure.Data.EFDbContext;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Users> Users { get; set; }
    public DbSet<ErrorLog> Errors { get; set; }
}
