using System;
using System.Collections.Concurrent;

namespace Newegg.EC.Kafka.Client
{
    public static class KafkaFactory
    {
        private static readonly ConcurrentDictionary<string, IKafkaClient> KafkaClients = new ConcurrentDictionary<string, IKafkaClient>();
        private static readonly object LockObj = new object();

        /// <summary>
        /// Create kafka client.
        /// </summary>
        /// <param name="brokerList">Broker list.</param>
        /// <returns>Kafka client.</returns>
        public static IKafkaClient CreateClient(string brokerList)
        {
            if (string.IsNullOrWhiteSpace(brokerList))
            {
                throw new ArgumentNullException("Broker list is empty or null.");
            }

            lock (LockObj)
            {
                if (KafkaClients.ContainsKey(brokerList))
                {
                    return KafkaClients[brokerList];
                }

                var client = new KafkaClient(brokerList);
                KafkaClients.TryAdd(brokerList, client);
                return client;
            }
        }
    }
}
