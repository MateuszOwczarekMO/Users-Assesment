using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Users.Infrastucture.Storeage
{
    public static class ServiceInjector
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<UsersDbContext>(
                options => options.UseSqlite(connectionString));

            return services;
        }
    }
}
