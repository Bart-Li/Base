using System.Collections.Concurrent;
using System.Linq;
using Newegg.EC.Zookeeper.Client.Impl;

namespace Newegg.EC.Zookeeper.Client
{
    public static class ZookeeperFactory
    {
        private static readonly ZookeeperConfig ZookeeperConfig = ZookeeperCluster.GetZookeeperConfig();
        private static readonly ConcurrentDictionary<string, IZookeeperClient> ZookeeperClients = new ConcurrentDictionary<string, IZookeeperClient>();
        private static readonly object LockObj = new object();

        /// <summary>
        /// Create zookeeper client instance.
        /// </summary>
        /// <param name="connectionString">Connection string.</param>
        /// <param name="sessionTimeout">Session timeout(s).</param>
        /// <returns>Zookeeper client instance.</returns>
        public static IZookeeperClient CreateClient(string connectionString, int sessionTimeout)
        {
            return new ZookeeperClient(connectionString, sessionTimeout);
        }

        /// <summary>
        /// Create zookeeper client by cluster name.
        /// </summary>
        /// <param name="clusterName">Cluster name(GDEV,GQC,WH7,E4,E11).</param>
        /// <returns>Zookeeper client instance.</returns>
        public static IZookeeperClient CreateClient(string clusterName)
        {
            if (string.IsNullOrWhiteSpace(clusterName))
            {
                clusterName = ZookeeperConfig.DefaultCluster;
            }

            lock (LockObj)
            {
                var zookeeperCluster = ZookeeperConfig.Clusters.FirstOrDefault(i => i.ClusterName == clusterName) ??
                                       ZookeeperConfig.Clusters.FirstOrDefault(i => i.ClusterName == ZookeeperConfig.DefaultCluster);

                if (ZookeeperClients.ContainsKey(zookeeperCluster.ClusterName))
                {
                    return ZookeeperClients[zookeeperCluster.ClusterName];
                }

                var keeper = CreateClient(zookeeperCluster.ConnectionString, zookeeperCluster.SessionTimeout);
                ZookeeperClients.TryAdd(zookeeperCluster.ClusterName, keeper);
                return keeper;
            }
        }
    }
}
