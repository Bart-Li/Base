using System;
using System.Collections.Generic;
using System.Linq;
using Newegg.EC.Core.BizUnit;
using Newegg.EC.Core.Configuration;
using Newegg.EC.Core.Context;
using Newegg.EC.Core.Host.Config;

namespace Newegg.EC.Core.Host.Impl
{
    /// <summary>
    /// Service host repository.
    /// </summary>
    [AutoSetupService(typeof(IServiceHostRepository))]
    public class DefaultServiceHostRepository : IServiceHostRepository
    {
        /// <summary>
        /// Configuration manager.
        /// </summary>
        private readonly IConfigurationManager _configManager;

        /// <summary>
        /// Request context.
        /// </summary>
        private readonly IRequestContext _requestContext;

        /// <summary>
        /// Biz unit.
        /// </summary>
        private readonly IBizUnit _bizUnit;

        /// <summary>
        /// Random.
        /// </summary>
        private readonly Random _random;

        /// <summary>
        /// Server host pepository.
        /// </summary>
        /// <param name="requestContext">Request context.</param>
        /// <param name="configManager">Config manager.</param>
        /// <param name="bizUnit">Biz unit.</param>
        public DefaultServiceHostRepository(IRequestContext requestContext, IConfigurationManager configManager, IBizUnit bizUnit)
        {
            this._requestContext = requestContext;
            this._configManager = configManager;
            this._bizUnit = bizUnit;
            this._random = new Random();
        }

        /// <summary>
        /// Get service host by service name.
        /// </summary>
        /// <param name="serviceName">Service name.</param>
        /// <returns>Service host.</returns>
        public ServiceHostUnit GetService(string serviceName)
        {
            var allServiceHost = GetAllService(serviceName);
            if (allServiceHost != null && !allServiceHost.IsNullOrEmpty())
            {
                return allServiceHost.ToArray()[this._random.Next(0, allServiceHost.Count)];
            }

            return null;
        }

        /// <summary>
        /// Get all service host by service name.
        /// </summary>
        /// <param name="serviceName">Service name.</param>
        /// <returns>All service host.</returns>
        public IList<ServiceHostUnit> GetAllService(string serviceName)
        {
            var serviceUnit = this.GetServiceUnit(serviceName);
            if (serviceUnit != null && !serviceUnit.Host.IsNullOrEmpty())
            {
                var channel = this._requestContext.ClientChannel;
                var serviceHosts = serviceUnit.Host.FindAll(h => h.Channel.Equals(channel, StringComparison.OrdinalIgnoreCase));

                if (serviceHosts.IsNullOrEmpty())
                {
                    throw new InvalidOperationException(string.Format("No configured hosts for channel '{0}', service name '{1}'", channel, serviceName));
                }

                return serviceHosts;
            }

            return null;
        }

        /// <summary>
        /// Get service unit config.
        /// </summary>
        /// <param name="serviceName">Service name.</param>
        /// <returns>Service unit.</returns>
        private ServiceUnit GetServiceUnit(string serviceName)
        {
            ServiceUnit service = null;
            var configName = "ServiceHost";
            if (this._bizUnit != null && !string.IsNullOrWhiteSpace(this._bizUnit.CountryCode))
            {
                configName = string.Format("ServiceHost-{0}", this._bizUnit.CountryCode);
            }

            var serviceConfig = this._configManager.GetConfigFromService<ServiceListConfig>(configName) ?? this._configManager.GetConfigFromService<ServiceListConfig>("ServiceHost");

            if (serviceConfig != null && !serviceConfig.Services.IsNullOrEmpty())
            {
                service = serviceConfig.Services.FirstOrDefault(s => s.Name.Equals(serviceName, StringComparison.OrdinalIgnoreCase));
            }

            return service;
        }
    }
}
