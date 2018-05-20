namespace Newegg.EC.Core.Configuration
{
    /// <summary>
    /// Configuration manager.
    /// </summary>
    public interface IConfigurationManager
    {
        /// <summary>
        /// Get an instance of the given configuration from config service.
        /// </summary>
        /// <typeparam name="TConfig">Type of configuration.</typeparam>
        /// <param name="configKey">Config file key.</param>
        /// <param name="nodeDataType">Node data type,Default is json.</param>
        /// <returns>The requested configuration.</returns>
        TConfig GetConfigFromServiceByKey<TConfig>(string configKey, NodeDataType nodeDataType = NodeDataType.Json) where TConfig : class, new();

        /// <summary>
        /// Get an instance of the given configuration from config service.
        /// </summary>
        /// <typeparam name="TConfig">Type of configuration.</typeparam>
        /// <param name="configName">Config name.</param>
        /// <param name="nodeDataType">Node data type.</param>
        /// <returns>The requested configuration.</returns>
        TConfig GetConfigFromService<TConfig>(string configName, NodeDataType nodeDataType = NodeDataType.Json) where TConfig : class, new();

        /// <summary>
        /// Get an instance of the given configuration from config service.
        /// </summary>
        /// <typeparam name="TConfig">Type of configuration.</typeparam>
        /// <param name="configName">Config name.</param>
        /// <param name="systemName">System name.</param>
        /// <param name="nodeDataType">Node data type.</param>
        /// <returns>The requested configuration.</returns>
        TConfig GetConfigFromService<TConfig>(string configName, string systemName, NodeDataType nodeDataType = NodeDataType.Json) where TConfig : class, new();

        /// <summary>
        /// Get an instance of the given configuration from file.
        /// </summary>
        /// <typeparam name="TConfig">Type of configuraion.</typeparam>
        /// <param name="configKey">Config file key.</param>
        /// <returns>The requested configuration.</returns>
        TConfig GetConfigFromFileByKey<TConfig>(string configKey) where TConfig : class, new();

        /// <summary>
        /// Get an instance of the given configuration from section.
        /// </summary>
        /// <typeparam name="TConfig">Type of configuraion.</typeparam>
        /// <param name="sectionName">Section name.</param>
        /// <returns>The requested configuration.</returns>
        TConfig GetSection<TConfig>(string sectionName) where TConfig : class, new();
    }
}
