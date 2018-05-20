using Microsoft.Extensions.DependencyInjection;
using Newegg.EC.Core.Configuration;
using Newegg.EC.Core.IOC;
using NLog.Web;

namespace Newegg.EC.Core
{
    /// <summary>
    /// EC base service extensions.
    /// </summary>
    public static class FrameworkServiceExtensions
    {
        /// <summary>
        /// Add EC framework base service.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <returns>Service collection.</returns>
        public static IServiceCollection AddFrameworkService(this IServiceCollection services)
        {
            NLogBuilder.ConfigureNLog("nlog.config");

            services.AddConfigurationService()
                .AddAutoSetupService();
                           
            return services;
        }
    }
}
