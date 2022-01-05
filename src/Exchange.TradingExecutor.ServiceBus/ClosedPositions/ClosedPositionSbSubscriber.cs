using MyServiceBus.Abstractions;
using MyServiceBus.TcpClient;
using SimpleTrading.ServiceBus.CommonUtils;
using SimpleTrading.ServiceBus.CommonUtils.Serializers;

namespace StatelessTradingExecutor.ServiceBus.ClosedPositions
{
    public class ClosedPositionSbSubscriber : Subscriber<ClosedPositionSbModel>
    {
        public ClosedPositionSbSubscriber(MyServiceBusTcpClient client, string queueName, TopicQueueType type,
            bool isChunk) :
            base(client, TopicNames.ClosedPositions, queueName, type,
                bytes => bytes.ByteArrayToServiceBusContract<ClosedPositionSbModel>(), isChunk)
        {
        }
    }
}