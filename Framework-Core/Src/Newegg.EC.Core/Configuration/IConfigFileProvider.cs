using Newegg.EC.Core.Configuration.Impl;

namespace Newegg.EC.Core.Configuration
{
    /// <summary>
    /// Config file provider.
    /// </summary>
    public interface IConfigFileProvider
    {
        /// <summary>
        /// Get system config.
        /// </summary>
        /// <returns></returns>
        SystemConfig GetSystemConfig();

        /// <summary>
        /// Get config from file.
        /// </summary>
        /// <typeparam name="TConfigType">Config Type.</typeparam>
        /// <param name="configName">Config name.</param>
        /// <returns>Config instance.</returns>
        TConfigType GetConfig<TConfigType>(string configName) where TConfigType : class, new();

        /// <summary>
        /// Get config section node.
        /// </summary>
        /// <typeparam name="TConfigType">Config type.</typeparam>
        /// <param name="sectionName">Section name.</param>
        /// <returns>Config instance.</returns>
        TConfigType GetSection<TConfigType>(string sectionName) where TConfigType : class, new();
    }
}
