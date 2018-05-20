using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace Newegg.EC.Kafka.Client
{
    /// <summary>
    /// Kafka client.
    /// </summary>
    internal class KafkaClient : IKafkaClient
    {
        /// <summary>
        /// Create kafka client instance.
        /// </summary>
        public KafkaClient(string brokerList)
        {
            this.BrokerList = brokerList;            
        }

        /// <summary>
        /// Kafka broker list.Split by','.
        /// </summary>
        public string BrokerList { get; set; }

        /// <summary>
        /// Consumer group name.
        /// </summary>
        public string ConsumerGroupName { get; set; }

        /// <summary>
        /// Offset commit interval(millisecond).
        /// </summary>
        public int OffsetCommitInterval { get; set; } = 5000;

        /// <summary>
        /// Commint interval(millisecond).
        /// </summary>
        public int CommitInterval { get; set; } = 100;

        /// <summary>
        /// Gets or sets key type.
        /// </summary>
        public KeyType KeyType { get; set; } = KeyType.Null;

        /// <summary>
        ///     Raised when a new message is avaiable for consumption. NOT raised when Consumer.Consume
        ///     is used for polling (only when Consmer.Poll is used for polling). NOT raised when the
        ///     message has an Error (OnConsumeError is raised in that case).
        /// </summary>
        public event EventHandler<string> OnMessage;

        /// <summary>
        /// Start consumse message.
        /// </summary>
        public void Consume()
        {


            //using (var kafkaConsumer = new Consumer<Null, string>(config, null, new StringDeserializer(Encoding.UTF8)))
            //{
            //    kafkaConsumer.OnMessage += new SmtpLogConsumer().ConsumerMessage;
            //    kafkaConsumer.OnPartitionsAssigned += (_, partitions) =>
            //    {
            //        Console.WriteLine($"Assigned partitions: [{string.Join(", ", partitions)}]");
            //        kafkaConsumer.Assign(partitions);
            //    };
            //    kafkaConsumer.Subscribe("EC_Email_Smtp");
            //    Console.WriteLine($"Subscribed to: [{string.Join(", ", kafkaConsumer.Subscription)}]");

            //    while (true)
            //    {
            //        kafkaConsumer.Poll(TimeSpan.FromMilliseconds(100));
            //    }
            //}
        }

        //private Consumer<TKey, string> CreateConsumer<TKey>()
        //{
        //    var config = new Dictionary<string, object>
        //    {
        //        { "group.id", this.ConsumerGroupName },
        //        { "enable.auto.commit", true },
        //        { "auto.commit.interval.ms", this.CommitInterval },
        //        { "bootstrap.servers", this.BrokerList },
        //        { "default.topic.config", new Dictionary<string, object>()
        //            {
        //                { "auto.offset.reset", "smallest" }
        //            }
        //        }
        //    };

        //    switch (this.KeyType)
        //    {
        //        case KeyType.Long:
        //            return new Consumer<long, string>(config, new LongDeserializer(), new StringDeserializer(Encoding.UTF8));
        //            //return new IngoreDeserializer();
        //    }
        //}
    }
}
