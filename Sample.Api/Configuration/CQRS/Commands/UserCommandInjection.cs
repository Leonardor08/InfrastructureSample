using Sample.Application.Commands;
using Sample.Application.Interfaces.Validations;
using Sample.Infraestructure.Validations;
using System.Reflection;

namespace Sample.Api.Configuration.CQRS.Commands
{
    public static class UserCommandInjection
    {
        public static IServiceCollection AddCommandsDependency(this IServiceCollection services)
        {
            services.AddTransient<ICreateUserValidations, CreateUserValidations>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateUserCommand>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<EditUserCommand>());
            return services;
        }
    }
}
