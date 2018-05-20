using System;
using Newegg.EC.Core.Serialization;
using Newegg.EC.Zookeeper.Client;

namespace Newegg.EC.Core.Configuration.Impl
{
    /// <summary>
    /// Default config service provider.
    /// </summary>
    [AutoSetupService(typeof(IConfigServiceProvider))]
    public class DefaultConfigServiceProvider : IConfigServiceProvider
    {
        private static IZookeeperClient _zookeeperClient;
        private static readonly object SyncObject = new object();
        private readonly ISerializer _serializer;
        private readonly ICurrentEnvironment _currentEnvironment;

        /// <summary>
        /// Default config service provider construction.
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="currentEnvironment"></param>
        public DefaultConfigServiceProvider(ISerializer serializer, ICurrentEnvironment currentEnvironment)
        {
            this._serializer = serializer;
            this._currentEnvironment = currentEnvironment;
        }

        /// <summary>
        /// Get config from config service.
        /// </summary>
        /// <typeparam name="TConfigType">Config Type.</typeparam>
        /// <param name="systemName">System name.</param>
        /// <param name="configName">Config name.</param>
        /// <param name="nodeDataType">Node date type.</param>
        /// <returns>Config instance.</returns>
        public TConfigType GetConfig<TConfigType>(string systemName, string configName, NodeDataType nodeDataType = NodeDataType.Json)
        {
            var client = GetZookeeperClient();
            if (client != null)
            {
                var path = $"/{systemName}/{configName}";
                if (client.Exists(path))
                {
                    var data = client.GetData(path);
                    return DeserializeData<TConfigType>(data, nodeDataType);
                }                
            }

            return default(TConfigType);
        }

        /// <summary>
        /// Watch data change, exec callback method.
        /// </summary>
        /// <typeparam name="TConfigType">Config type.</typeparam>
        /// <param name="systemName">System name.</param>
        /// <param name="configName">Config name.</param>
        /// <param name="callback">Callback action.</param>
        /// <param name="dataType">Node data type, Default is json.</param>
        public void WatchDataChange<TConfigType>(string systemName, string configName, Action<TConfigType> callback, NodeDataType dataType = NodeDataType.Json)
        {
            var client = GetZookeeperClient();
            if (client != null)
            {
                client.Watch($"/{systemName}/{configName}", context =>
                {
                    var data = context.GetData();
                    callback(DeserializeData<TConfigType>(data, dataType));
                });
            }
        }

        /// <summary>
        /// Create Zookeeper client from config.
        /// </summary>
        /// <returns>Zookeeper client</returns>
        private IZookeeperClient GetZookeeperClient()
        {
            if (_zookeeperClient == null)
            {
                lock (SyncObject)
                {
                    if (_zookeeperClient == null)
                    {
                        _zookeeperClient = ZookeeperFactory.CreateClient(this._currentEnvironment.Location);
                    }
                }
            }

            return _zookeeperClient;
        }

        /// <summary>
        /// Deserialize config data.
        /// </summary>
        /// <typeparam name="TConfigType">Config type.</typeparam>
        /// <param name="data">Config value data.</param>
        /// <param name="dataType">Node data type.</param>
        /// <returns>Config type instance.</returns>
        private TConfigType DeserializeData<TConfigType>(string data, NodeDataType dataType)
        {
            if (string.IsNullOrEmpty(data))
            {
                return default(TConfigType);
            }

            if (dataType == NodeDataType.Json)
            {
                return this._serializer.DeserializeObject<TConfigType>(data);
            }
            else if (dataType == NodeDataType.Xml)
            {
                return this._serializer.DeserializeXml<TConfigType>(data);
            }
            else
            {
                return this._serializer.DeserializeObject<TConfigType>(this._serializer.SerializeObject(data));
            }
        }
    }
}
