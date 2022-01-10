using System.Threading.Tasks;
using DotNetCoreDecorators;
using MyServiceBus.TcpClient;
using SimpleTrading.ServiceBus.CommonUtils.Serializers;

namespace StatelessTradingExecutor.ServiceBus.ClosedPositions
{
    public class ClosedPositionSbPublisher : IPublisher<ClosedPositionSbModel>
    {
        private readonly MyServiceBusTcpClient _client; 
        
        public ClosedPositionSbPublisher(MyServiceBusTcpClient client)
        {
            _client = client;
            _client.CreateTopicIfNotExists(TopicNames.ClosedPosition);
        }
 
        public ValueTask PublishAsync(ClosedPositionSbModel contract)
        {
            var bytesToSend = contract.ServiceBusContractToByteArray();
            var task = _client.PublishAsync(TopicNames.ClosedPosition, bytesToSend, false);
            
            return new ValueTask(task);
        }
    }
}