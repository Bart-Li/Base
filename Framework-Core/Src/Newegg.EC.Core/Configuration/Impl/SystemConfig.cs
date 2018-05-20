using System.Collections.Generic;

namespace Newegg.EC.Core.Configuration.Impl
{
    /// <summary>
    /// System config.
    /// </summary>
    public class SystemConfig
    {
        /// <summary>
        /// Default system name.
        /// </summary>
        public string DefaultSystemName { get; set; }

        /// <summary>
        /// Gets or sets config file list.
        /// </summary>
        public List<ConfigFile> ConfigFileList { get; set; }
    }

    public class ConfigFile
    {
        /// <summary>
        /// Gets or sets config key.
        /// </summary>
        public string ConfigKey { get; set; }

        /// <summary>
        /// Gets or sets config node name.
        /// </summary>
        public string ConfigName { get; set; }

        /// <summary>
        /// Gets or sets file path.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or set config service system name.
        /// </summary>
        public string SystemName { get; set; }
    }
}
