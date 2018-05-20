using System;
using System.Collections.Generic;

namespace Newegg.EC.Zookeeper.Client
{
    public static class ZookeeperCluster
    {
        public static ZookeeperConfig GetZookeeperConfig()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (!string.IsNullOrWhiteSpace(env) && Clusters.ContainsKey(env))
            {
                return Clusters[env];
            }

            return Clusters["GDEV"];
        }

        public static readonly IReadOnlyDictionary<string, ZookeeperConfig> Clusters = new Dictionary<string, ZookeeperConfig>(StringComparer.OrdinalIgnoreCase)
        {
            { "GDEV",new ZookeeperConfig
                {
                    Clusters = new List<Cluster>
                    {
                        new Cluster
                        {
                             ClusterName = "GDEV",
                             ConnectionString = "10.16.75.23:8481, 10.16.75.25:8481, 10.16.75.26:8481",
                             SessionTimeout = 5000
                        }
                    },
                    DefaultCluster = "GDEV"
                }
            },
            { "GQC",new ZookeeperConfig
                {
                    Clusters = new List<Cluster>
                    {
                        new Cluster
                        {
                             ClusterName = "GQC",
                             ConnectionString = "10.1.24.130:8481",
                             SessionTimeout = 5000
                        }
                    },
                    DefaultCluster = "GQC"
                }
            },
            { "PRE",new ZookeeperConfig
                {
                    Clusters = new List<Cluster>
                    {
                        new Cluster
                        {
                             ClusterName = "WH7",
                             ConnectionString = "config7.newegg.org:80",
                             SessionTimeout = 5000
                        },
                        new Cluster
                        {
                             ClusterName = "E11",
                             ConnectionString = "config11.newegg.org:80",
                             SessionTimeout = 5000
                        },
                        new Cluster
                        {
                             ClusterName = "E4",
                             ConnectionString = "config4.newegg.org:80",
                             SessionTimeout = 5000
                        }
                    },
                    DefaultCluster = "WH7"
                }
            },
            { "PRD",new ZookeeperConfig
                {
                    Clusters = new List<Cluster>
                    {
                        new Cluster
                        {
                             ClusterName = "WH7",
                             ConnectionString = "config7.newegg.org:80",
                             SessionTimeout = 5000
                        },
                        new Cluster
                        {
                             ClusterName = "E11",
                             ConnectionString = "config11.newegg.org:80",
                             SessionTimeout = 5000
                        },
                        new Cluster
                        {
                             ClusterName = "E4",
                             ConnectionString = "config4.newegg.org:80",
                             SessionTimeout = 5000
                        }
                    },
                    DefaultCluster = "WH7"
                }
            }
        };
    }
}
