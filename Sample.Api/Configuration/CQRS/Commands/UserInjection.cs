using Sample.Application.Commands;
using Sample.Application.Queries;
using Sample.Domain.Interfaces.Validations;
using Sample.Infraestructure.Validations;

namespace Sample.Api.Configuration.CQRS.Commands
{
    public static class UserInjection
    {
        public static IServiceCollection AddUsersDependency(this IServiceCollection services)
        {
            services.AddTransient<ICreateUserValidations, CreateUserValidations>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateUserCommand>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<EditUserCommand>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetUsersQuery>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetUserByIdQuery>());
            return services;
        }
    }
}
