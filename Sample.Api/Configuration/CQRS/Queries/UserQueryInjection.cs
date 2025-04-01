using Sample.Application.Interfaces.Validations;
using Sample.Application.Queries;
using Sample.Infraestructure.Validations;

namespace Sample.Api.Configuration.CQRS.Queries
{
    public static class UserQueryInjection
    {
        public static IServiceCollection AddQueriesDependency(this IServiceCollection services)
        {
            services.AddTransient<ICreateUserValidations, CreateUserValidations>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetActiveUsersQuery>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetUsersQuery>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetUserByIdQuery>());
            return services;
        }
    }
}
