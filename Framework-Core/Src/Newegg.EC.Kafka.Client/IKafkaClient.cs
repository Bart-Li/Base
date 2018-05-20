using System;

namespace Newegg.EC.Kafka.Client
{
    /// <summary>
    /// Kafka client.
    /// </summary>
    public interface IKafkaClient
    {
        /// <summary>
        /// Kafka broker list.Split by','.
        /// </summary>
        string BrokerList { get; set; }

        /// <summary>
        /// Consumer group name.
        /// </summary>
        string ConsumerGroupName { get; set; }

        /// <summary>
        /// Offset commit interval(millisecond).
        /// </summary>
        int OffsetCommitInterval { get; set; }

        /// <summary>
        /// Commint interval(millisecond).
        /// </summary>
        int CommitInterval { get; set; }

        /// <summary>
        /// Gets or sets key type.
        /// </summary>
        KeyType KeyType { get; set; }

        /// <summary>
        /// Raised when a new message is avaiable for consumption. NOT raised when Consumer.Consume
        /// is used for polling (only when Consmer.Poll is used for polling). NOT raised when the
        /// message has an Error (OnConsumeError is raised in that case).
        /// </summary>
        event EventHandler<string> OnMessage;

        /// <summary>
        /// Start consumse message.
        /// </summary>
        void Consume();
    }
}
