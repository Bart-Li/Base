using System.Collections.Generic;

namespace Newegg.EC.Core.Configuration.Impl
{
    [AutoSetupService(typeof(IConfigAccessor))]
    public class DefaultConfigAccessor : IConfigAccessor
    {
        private readonly IConfigServiceProvider _configServiceProvider;
        private readonly IConfigFileProvider _configFileProvider;
        private static readonly IDictionary<string, object> ConfigCollection = new Dictionary<string, object>();

        public DefaultConfigAccessor(IConfigServiceProvider configServiceProvider, IConfigFileProvider configFileProvider)
        {
            this._configServiceProvider = configServiceProvider;
            this._configFileProvider = configFileProvider;
        }

        /// <summary>
        /// Get config value.
        /// </summary>
        /// <typeparam name="TConfigType">Config type.</typeparam>
        /// <param name="configDefinition">Config definition.</param>
        /// <param name="nodeDataType">Node data type.</param>
        /// <returns>Config instance.</returns>
        public TConfigType GetConfigValue<TConfigType>(IConfigDefinition configDefinition, NodeDataType nodeDataType = NodeDataType.Json) where TConfigType : class, new()
        {
            if (configDefinition == null)
            {
                return null;
            }

            var configCollectionKey = $"{configDefinition.SystemName}_{configDefinition.ConfigName}".ToLower();
            if (ConfigCollection.ContainsKey(configCollectionKey))
            {
                return ConfigCollection[configCollectionKey] as TConfigType;
            }

            TConfigType configValue;

            if (configDefinition.IsFromService)
            {
                configValue = this._configServiceProvider.GetConfig<TConfigType>(configDefinition.SystemName, configDefinition.ConfigName, nodeDataType);

                this._configServiceProvider.WatchDataChange<TConfigType>(configDefinition.SystemName, configDefinition.ConfigName,
                    value => this.AddOrUpdateConfigCollection(configDefinition, value), nodeDataType);
            }
            else
            {
                configValue = this._configFileProvider.GetConfig<TConfigType>(configDefinition.ConfigName);                
            }

            this.AddOrUpdateConfigCollection(configDefinition, configValue);
            return configValue;
        }

        /// <summary>
        /// Get config section value.
        /// </summary>
        /// <typeparam name="TConfigType">Config type.</typeparam>
        /// <param name="sectionName">Section name.</param>
        /// <returns>Config instance.</returns>
        public TConfigType GetConfigSection<TConfigType>(string sectionName) where TConfigType : class, new()
        {
            return this._configFileProvider.GetSection<TConfigType>(sectionName);
        }

        /// <summary>
        /// Add or update config collection.
        /// </summary>
        /// <param name="configDefinition">Config definition.</param>
        /// <param name="configValue">Config value.</param>
        private void AddOrUpdateConfigCollection(IConfigDefinition configDefinition, object configValue)
        {
            if (configValue == null)
            {
                return;
            }

            var configCollectionKey = $"{configDefinition.SystemName}_{configDefinition.ConfigName}".ToLower();

            if (ConfigCollection.ContainsKey(configCollectionKey))
            {
                ConfigCollection[configCollectionKey] = configValue;
            }
            else
            {
                ConfigCollection.Add(configCollectionKey, configValue);
            }
        }
    }
}
