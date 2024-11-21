using CWB.CommonUtils.Common;
using CWB.CommonUtils.KafkaConfigs;
using System.Threading.Tasks;

namespace CWB.CommonUtils.MessageBrokers
{
    public interface IMessageBroker
    {
        Task<bool> SendAsync(KafkaConfig config, MessageInfo messageInfo);
        Task CreateTopicAsync(KafkaConfig config);
    }
}
