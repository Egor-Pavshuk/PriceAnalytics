using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PriceAnalytics.DAL;
using PriceAnalytics.Core.Interfaces;

namespace PriceAnalytics.Infrastructure
{
    public static class DependencyInjection
    {
        private const string DEFAULT_CONNECTION = "DefaultConnection";
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PriceAnalyticsDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString(DEFAULT_CONNECTION)));

            services.AddScoped<IPriceAnalyticsRepository, PriceAnalyticsRepository>();

            return services;
        }
    }
}
