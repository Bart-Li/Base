using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newegg.EC.Core.Configuration.Impl;

namespace Newegg.EC.Core.Configuration
{
    /// <summary>
    /// Configuration service extensions.
    /// </summary>
    public static class ConfigServiceExtensions
    {
        /// <summary>
        /// Add configuration service.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <returns>Service collection.</returns>
        internal static IServiceCollection AddConfigurationService(this IServiceCollection services)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true);

            SystemConfig systemConfig = configurationBuilder.Build().GetSection("SystemConfig").Get<SystemConfig>();
            if (systemConfig != null && !systemConfig.ConfigFileList.IsNullOrEmpty())
            {
                systemConfig.ConfigFileList.ForEach(file =>
                {
                    if (!string.IsNullOrWhiteSpace(file.FilePath))
                    {
                        configurationBuilder.AddJsonFile(file.FilePath, false, true);
                    }
                });
            }

            IConfiguration configuration = configurationBuilder.Build();
            services.AddSingleton(configuration);
            return services;
        }
    }
}
