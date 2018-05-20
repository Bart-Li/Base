using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Newegg.EC.Core.Configuration.Impl
{
    [AutoSetupService(typeof(IConfigFileProvider))]
    public class DefaultConfigFileProvider : IConfigFileProvider
    {
        private readonly IServiceCollection _service;
        private readonly IConfiguration _configuration;

        public DefaultConfigFileProvider(IServiceCollection service, IConfiguration configuration)
        {
            this._service = service;
            this._configuration = configuration;
        }

        /// <summary>
        /// Get system config.
        /// </summary>
        /// <returns></returns>
        public SystemConfig GetSystemConfig()
        {
            return this.GetSection<SystemConfig>("SystemConfig");
        }

        /// <summary>
        /// Get config from file.
        /// </summary>
        /// <typeparam name="TConfigType">Config Type.</typeparam>
        /// <param name="configName">Config name.</param>
        /// <returns>Config instance.</returns>
        public TConfigType GetConfig<TConfigType>(string configName) where TConfigType : class, new()
        {
            var configValue = this._service
                .AddOptions()
                .Configure<TConfigType>(this._configuration.GetSection(configName))
                .BuildServiceProvider()
                .GetService<IOptions<TConfigType>>()
                .Value;

            return configValue;
        }

        /// <summary>
        /// Get config section node.
        /// </summary>
        /// <typeparam name="TConfigType">Config type.</typeparam>
        /// <param name="sectionName">Section name.</param>
        /// <returns>Config instance.</returns>
        public TConfigType GetSection<TConfigType>(string sectionName) where TConfigType : class, new()
        {
            return this._configuration.GetSection(sectionName).Get<TConfigType>();
        }
    }
}
