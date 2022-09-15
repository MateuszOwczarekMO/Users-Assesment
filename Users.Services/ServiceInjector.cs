using Microsoft.Extensions.DependencyInjection;
using Users.Services.DateTimeProviderService;
using Users.Services.GenerateAgeService;

namespace Users.Services
{
    public static class ServiceInjector
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IGenerateAgeFromDOB, GenerateAgeFromDOB>();
            return services;
        }
    }
}
