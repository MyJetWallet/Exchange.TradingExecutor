using MyServiceBus.Abstractions;
using MyServiceBus.TcpClient;
using SimpleTrading.ServiceBus.CommonUtils;
using SimpleTrading.ServiceBus.CommonUtils.Serializers;

namespace StatelessTradingExecutor.ServiceBus.ClosePositionCommands
{
    public class ClosePositionCommandSbSubscriber : Subscriber<ClosePositionSbCommand>
    {
        public ClosePositionCommandSbSubscriber(MyServiceBusTcpClient client, string queueName, TopicQueueType type,
            bool isChunk) :
            base(client, TopicNames.ClosePositionCommand, queueName, type,
                bytes => bytes.ByteArrayToServiceBusContract<ClosePositionSbCommand>(), isChunk)
        {
        }
    }
}