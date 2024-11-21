using Confluent.Kafka;
using Confluent.Kafka.Admin;
using CWB.CommonUtils.Common;
using CWB.CommonUtils.KafkaConfigs;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CWB.CommonUtils.MessageBrokers
{
    public class MessageBroker : IMessageBroker
    {
        public async Task<bool> SendAsync(KafkaConfig config, MessageInfo messageInfo)
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = config.Server
            };
            using var p = new ProducerBuilder<string, string>(producerConfig).Build();
            var messageToSend = new Message<string, string>
            {
                Value = JsonConvert.SerializeObject(messageInfo),
                Key = messageInfo.MessageType
            };
            var dr = await p.ProduceAsync(config.Topic, messageToSend);
            return (dr.Status == PersistenceStatus.Persisted);
        }

        public async Task CreateTopicAsync(KafkaConfig config)
        {
            using (var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = config.Server }).Build())
            {
                try
                {
                    await adminClient.CreateTopicsAsync(new TopicSpecification[] {
                        new TopicSpecification { Name = config.Topic, ReplicationFactor = 1, NumPartitions = 1 } });
                }
                catch (CreateTopicsException e)
                {
                    //ignore if topic exists
                }
            }
        }
    }
}
