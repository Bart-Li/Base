using System.Collections.Generic;

namespace Newegg.EC.Zookeeper.Client
{
    public class ZookeeperConfig
    {
        /// <summary>
        /// Gets or sets default cluster.
        /// </summary>
        public string DefaultCluster { get; set; }

        /// <summary>
        /// Gets or sets clusters.
        /// </summary>
        public List<Cluster> Clusters { get; set; }
    }

    public class Cluster
    {
        /// <summary>
        /// Cluster name.
        /// </summary>
        public string ClusterName { get; set; }

        /// <summary>
        /// Connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Session time out.
        /// </summary>
        public int SessionTimeout { get; set; } = 1000;
    }
}
