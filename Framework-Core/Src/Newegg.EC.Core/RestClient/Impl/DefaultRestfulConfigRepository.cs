using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newegg.EC.Core.Configuration;
using Newegg.EC.Core.RestClient.Config;

namespace Newegg.EC.Core.RestClient.Impl
{
    /// <summary>
    /// Default restful service config repository.
    /// </summary>
    [AutoSetupService(typeof(IRestfulConfigRepository))]
    public class DefaultRestfulConfigRepository : IRestfulConfigRepository
    {
        /// <summary>
        /// Configuration manager.
        /// </summary>
        private readonly IConfigurationManager _configurationManager;

        /// <summary>
        /// Restful service config repository.
        /// </summary>
        /// <param name="configurationManager">Configuration manager.</param>
        public DefaultRestfulConfigRepository(IConfigurationManager configurationManager)
        {
            this._configurationManager = configurationManager;
        }

        /// <summary>
        /// Get default timeout.
        /// </summary>
        /// <returns>Default timeout.</returns>
        public TimeSpan DefaultTimeout
        {
            get
            {
                TimeSpan result = default(TimeSpan);
                RestfulServiceConfig config = this.GetConfig();
                if (config != null)
                {
                    result = config.DefaultTimeoutSpan;
                }

                return result;
            }
        }

        /// <summary>
        /// Get default max response size.
        /// </summary>
        /// <returns>Default response size.</returns>
        public long DefaultMaxResponseSize
        {
            get
            {
                long result = default(long);
                RestfulServiceConfig config = this.GetConfig();
                if (config != null)
                {
                    result = config.DefaultMaxResponseSize;
                }

                return result;
            }
        }

        /// <summary>
        /// A value indicating whether remove default parameters.
        /// </summary>
        public bool RemoveDefaultParameter
        {
            get
            {
                bool result = false;
                RestfulServiceConfig config = this.GetConfig();
                if (config != null)
                {
                    result = config.RemoveDefaultParameter;
                }

                return result;
            }
        }

        /// <summary>
        /// Get restful service resource unit.
        /// </summary>
        /// <param name="resourceKey">Resource key.</param>
        /// <returns>Restful service resource unit.</returns>
        public RestfulServiceResourceUnit GetResource(string resourceKey)
        {
            if (string.IsNullOrWhiteSpace(resourceKey))
            {
                throw new ArgumentException("Resource key is null, empty or whitespace", "resourceKey");
            }

            RestfulServiceConfig config = this.GetConfig();

            if (config == null)
            {
                throw new OperationCanceledException(@"RestfulServiceConfig is null. Please check this config file.");
            }

            if (config.Resources == null || !config.Resources.Any())
            {
                throw new OperationCanceledException(@"RestfulServiceConfig resources node is null. Please check this config file.");
            }

            return config.Resources.FirstOrDefault(resource => string.Equals(resource.Key, resourceKey, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Get restful service setting unit.
        /// </summary>
        /// <param name="settingKey">Setting key.</param>
        /// <returns>Restfu service setting unit.</returns>
        public RestfulServiceSettingUnit GetSetting(string settingKey)
        {
            if (string.IsNullOrWhiteSpace(settingKey))
            {
                throw new ArgumentException("Setting key is null, empty or whitespace", "settingKey");
            }

            RestfulServiceConfig config = this.GetConfig();

            if (config == null)
            {
                throw new OperationCanceledException(@"RestfulServiceConfig is null. Please check this config file.");
            }

            if (config.SettingGroups == null || !config.SettingGroups.Any())
            {
                throw new OperationCanceledException(@"RestfulServiceConfig settingGroups node is null. Please check this config file.");
            }

            return config.SettingGroups.FirstOrDefault(setting => string.Equals(setting.Key, settingKey, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Get restful service resource unit.
        /// </summary>
        /// <param name="url">Resource url.</param>
        /// <returns>Restful service resource unite.</returns>
        public RestfulServiceResourceUnit GetResourceByUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("Resource url is null, empty or whitespace", "url");
            }

            RestfulServiceConfig config = this.GetConfig();

            if (config == null)
            {
                throw new OperationCanceledException(@"RestfulServiceConfig is null. Please check this config file.");
            }

            if (config.Resources == null || !config.Resources.Any())
            {
                throw new OperationCanceledException(@"RestfulServiceConfig resources node is null. Please check this config file.");
            }

            return config.Resources.FirstOrDefault(resource => string.Equals(resource.Url, url, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Get configuration.
        /// </summary>
        /// <returns>Restful service config.</returns>
        private RestfulServiceConfig GetConfig()
        {
            return this._configurationManager.GetConfigFromService<RestfulServiceConfig>("RestfulService");
        }
    }
}
