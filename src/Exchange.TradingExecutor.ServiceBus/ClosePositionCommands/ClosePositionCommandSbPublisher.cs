using System.Threading.Tasks;
using DotNetCoreDecorators;
using MyServiceBus.TcpClient;
using SimpleTrading.ServiceBus.CommonUtils.Serializers;

namespace StatelessTradingExecutor.ServiceBus.ClosePositionCommands
{
    public class ClosePositionCommandSbPublisher : IPublisher<ClosePositionSbCommand>
    {
        private readonly MyServiceBusTcpClient _client; 
        
        public ClosePositionCommandSbPublisher(MyServiceBusTcpClient client)
        {
            _client = client;
            _client.CreateTopicIfNotExists(TopicNames.ClosePositionCommand);
        }
 
        public ValueTask PublishAsync(ClosePositionSbCommand contract)
        {
            var bytesToSend = contract.ServiceBusContractToByteArray();
            var task = _client.PublishAsync(TopicNames.ClosePositionCommand, bytesToSend, false);
            
            return new ValueTask(task);
        }
    }
}