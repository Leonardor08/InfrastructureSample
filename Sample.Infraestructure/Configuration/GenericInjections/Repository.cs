﻿using Microsoft.Extensions.DependencyInjection;
using Sample.Domain.Interfaces;
using Sample.Infraestructure.Repository;

namespace Sample.Infraestructure.Configuration.GenericInjections;

public static class Repository
{
    public static IServiceCollection AddRepositoryDependency(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        return services;
    }
}
