using System;

namespace Newegg.EC.Core.Configuration
{
    /// <summary>
    /// Config service provider.
    /// </summary>
    public interface IConfigServiceProvider
    {
        /// <summary>
        /// Get config from config service.
        /// </summary>
        /// <typeparam name="TConfigType">Config Type.</typeparam>
        /// <param name="systemName">System name.</param>
        /// <param name="configName">Config name.</param>
        /// <param name="nodeDataType">Node date type.</param>
        /// <returns>Config instance.</returns>
        TConfigType GetConfig<TConfigType>(string systemName, string configName, NodeDataType nodeDataType = NodeDataType.Json);

        /// <summary>
        /// Watch data change, exec callback method.
        /// </summary>
        /// <typeparam name="TConfigType">Config type.</typeparam>
        /// <param name="systemName">System name.</param>
        /// <param name="configName">Config name.</param>
        /// <param name="callback">Callback action.</param>
        /// <param name="dataType">Node data type, Default is json.</param>
        void WatchDataChange<TConfigType>(string systemName, string configName, Action<TConfigType> callback, NodeDataType dataType = NodeDataType.Json);
    }
}
