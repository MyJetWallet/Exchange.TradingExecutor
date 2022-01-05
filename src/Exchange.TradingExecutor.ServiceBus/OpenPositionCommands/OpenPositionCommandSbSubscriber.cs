using MyServiceBus.Abstractions;
using MyServiceBus.TcpClient;
using SimpleTrading.ServiceBus.CommonUtils;
using SimpleTrading.ServiceBus.CommonUtils.Serializers;

namespace StatelessTradingExecutor.ServiceBus.OpenPositionCommands
{
    public class OpenPositionCommandSbSubscriber : Subscriber<OpenPositionSbCommand>
    {
        public OpenPositionCommandSbSubscriber(MyServiceBusTcpClient client, string queueName, TopicQueueType type,
            bool isChunk) :
            base(client, TopicNames.OpenPositionCommands, queueName, type,
                bytes => bytes.ByteArrayToServiceBusContract<OpenPositionSbCommand>(), isChunk)
        {
        }
    }
}