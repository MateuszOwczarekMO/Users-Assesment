using Microsoft.Extensions.DependencyInjection;
using Users.Repositories.Users;

namespace Users.Repositories
{
    public static class ServiceInjector
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
