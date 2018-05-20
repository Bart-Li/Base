using System;

namespace Newegg.EC.Core.Configuration.Impl
{
    /// <summary>
    /// Default configuration manager.
    /// </summary>
    [AutoSetupService(typeof(IConfigurationManager))]
    public class DefaultConfigurationManager : IConfigurationManager
    {
        private readonly IConfigAccessor _configAccessor;
        private readonly IConfigRepository _configRepository;

        public DefaultConfigurationManager(IConfigAccessor configAccessor, IConfigRepository configRepository)
        {
            this._configAccessor = configAccessor;
            this._configRepository = configRepository;
        }

        /// <summary>
        /// Get an instance of the given configuration from config service.
        /// </summary>
        /// <typeparam name="TConfig">Type of configuration.</typeparam>
        /// <param name="configKey">Config file key.</param>
        /// <param name="nodeDataType">Node data type.</param>
        /// <returns>The requested configuration.</returns>
        public TConfig GetConfigFromServiceByKey<TConfig>(string configKey, NodeDataType nodeDataType = NodeDataType.Json) where TConfig : class, new()
        {
            IConfigDefinition configFile = this._configRepository.GetConfigFileByKey(configKey);
            if (configFile != null)
            {
                configFile.IsFromService = true;
                return this._configAccessor.GetConfigValue<TConfig>(configFile, nodeDataType);
            }

            return null;
        }

        /// <summary>
        /// Get an instance of the given configuration from config service.
        /// </summary>
        /// <typeparam name="TConfig">Type of configuration.</typeparam>
        /// <param name="configName">Config name.</param>
        /// <param name="nodeDataType">Node data type.</param>
        /// <returns>The requested configuration.</returns>
        public TConfig GetConfigFromService<TConfig>(string configName, NodeDataType nodeDataType = NodeDataType.Json) where TConfig : class, new()
        {
            return GetConfigFromService<TConfig>(configName, string.Empty, nodeDataType);
        }

        /// <summary>
        /// Get an instance of the given configuration from config service.
        /// </summary>
        /// <typeparam name="TConfig">Type of configuration.</typeparam>
        /// <param name="configName">Config name.</param>
        /// <param name="systemName">System name.</param>
        /// <param name="nodeDataType">Node data type.</param>
        /// <returns>The requested configuration.</returns>
        public TConfig GetConfigFromService<TConfig>(string configName, string systemName, NodeDataType nodeDataType = NodeDataType.Json) where TConfig : class, new()
        {
            if (string.IsNullOrWhiteSpace(configName))
            {
                throw new ArgumentNullException("configName");
            }

            if (string.IsNullOrWhiteSpace(systemName))
            {
                systemName = this._configRepository.DefaultSystemName;
            }

            var configFile = new ConfigDefinition { SystemName = systemName, ConfigName = configName, IsFromService = true };
            return this._configAccessor.GetConfigValue<TConfig>(configFile, nodeDataType);
        }

        /// <summary>
        /// Get an instance of the given configuration from file.
        /// </summary>
        /// <typeparam name="TConfig">Type of configuraion.</typeparam>
        /// <param name="configKey">Config file key.</param>
        /// <returns>The requested configuration.</returns>
        public TConfig GetConfigFromFileByKey<TConfig>(string configKey) where TConfig : class, new()
        {
            var configFile = this._configRepository.GetConfigFileByKey(configKey);
            if (configFile != null)
            {
                return this._configAccessor.GetConfigValue<TConfig>(configFile);
            }

            return null;
        }

        /// <summary>
        /// Get an instance of the given configuration from section.
        /// </summary>
        /// <typeparam name="TConfig">Type of configuraion.</typeparam>
        /// <param name="sectionName">Section name.</param>
        /// <returns>The requested configuration.</returns>
        public TConfig GetSection<TConfig>(string sectionName) where TConfig : class, new()
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                return null;
            }

            return this._configAccessor.GetConfigSection<TConfig>(sectionName);
        }
    }
}
