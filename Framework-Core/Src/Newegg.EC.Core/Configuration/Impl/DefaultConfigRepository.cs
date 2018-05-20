using System;
using System.Collections.Generic;
using System.Linq;

namespace Newegg.EC.Core.Configuration.Impl
{
    [AutoSetupService(typeof(IConfigRepository))]
    public class DefaultConfigRepository : IConfigRepository
    {
        private readonly IConfigFileProvider _configFileProvider;

        /// <summary>
        /// Default config repostory.
        /// </summary>
        /// <param name="configFileProvider">Config file provider.</param>
        public DefaultConfigRepository(IConfigFileProvider configFileProvider)
        {
            this._configFileProvider = configFileProvider;
        }

        /// <summary>
        /// Default system name.
        /// </summary>
        public string DefaultSystemName
        {
            get
            {
                var systemConfig = this._configFileProvider.GetSystemConfig();
                if (systemConfig != null)
                {
                    return systemConfig.DefaultSystemName;
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Get config files.
        /// </summary>
        /// <returns></returns>
        public List<IConfigDefinition> GetConfigFiles()
        {
            var list = new List<IConfigDefinition>();
            var systemConfig = this._configFileProvider.GetSystemConfig();
            if (systemConfig?.ConfigFileList != null && systemConfig.ConfigFileList.Any())
            {
                systemConfig.ConfigFileList.ForEach(file =>
                {
                    list.Add(new ConfigDefinition
                    {
                        ConfigKey = file.ConfigKey,
                        ConfigName = file.ConfigName,
                        SystemName = !string.IsNullOrWhiteSpace(file.SystemName) ? file.SystemName : systemConfig.DefaultSystemName
                    });
                });
            }

            return list;
        }

        /// <summary>
        /// Get config file by key.
        /// </summary>
        /// <param name="configKey">Config key.</param>
        /// <returns>Config file.</returns>
        public IConfigDefinition GetConfigFileByKey(string configKey)
        {
            var configList = this.GetConfigFiles();
            if (!configList.IsNullOrEmpty())
            {
                return configList.Find(file => file.ConfigKey.Equals(configKey, StringComparison.OrdinalIgnoreCase));
            }

            return null;
        }        
    }
}
