﻿using Sample.Application.Commands;
using Sample.Application.Commands.Handlers;
using Sample.Application.Queries;
using Sample.Application.Queries.Handlers;
using Sample.Application.Validations;
using Sample.Domain.Interfaces.Commands;
using Sample.Domain.Interfaces.Queries;
using Sample.Domain.Models;
using Sample.Infraestructure.Validations;

namespace Sample.Api.Configuration.CQRS.Commands
{
    public static class UserInjection
    {
        public static IServiceCollection AddUsersDependency(this IServiceCollection services)
        {
            services.AddScoped(typeof(ICommandHandler<CreateUserCommand, Response>), typeof(CreateUserCommandHandler));
            services.AddScoped(typeof(IQueryHandler<GetUsersQuery, List<User>>), typeof(GetUsersQueryHandler));
            services.AddTransient<ICreateUserValidations, CreateUserValidations>();
			return services;
        }
    }
}
