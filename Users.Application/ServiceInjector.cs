using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Users.Application.Validators.Users;

namespace Users.Application
{
    public static class ServiceInjector
    {
        public static IServiceCollection AddApplicationCommandsQueries(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceInjector).Assembly);
            services.AddScoped<IUpdateUsersTableRequestValidator, UpdateUsersTableRequestValidator>();
            return services;
        }
    }
}
