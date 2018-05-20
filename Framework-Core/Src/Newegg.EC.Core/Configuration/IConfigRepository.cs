using System.Collections.Generic;

namespace Newegg.EC.Core.Configuration
{
    public interface IConfigRepository
    {   
        /// <summary>
        /// Default system name.
        /// </summary>
        string DefaultSystemName { get; }

        /// <summary>
        /// Get config files.
        /// </summary>
        /// <returns></returns>
        List<IConfigDefinition> GetConfigFiles();

        /// <summary>
        /// Get config file by key.
        /// </summary>
        /// <param name="configKey">Config key.</param>
        /// <returns>Config file.</returns>
        IConfigDefinition GetConfigFileByKey(string configKey);
    }
}
