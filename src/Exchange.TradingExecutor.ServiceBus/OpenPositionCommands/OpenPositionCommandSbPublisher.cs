using System.Threading.Tasks;
using DotNetCoreDecorators;
using MyServiceBus.TcpClient;
using SimpleTrading.ServiceBus.CommonUtils.Serializers;

namespace StatelessTradingExecutor.ServiceBus.OpenPositionCommands
{
    public class OpenPositionCommandSbPublisher : IPublisher<OpenPositionSbCommand>
    {
        private readonly MyServiceBusTcpClient _client; 
        
        public OpenPositionCommandSbPublisher(MyServiceBusTcpClient client)
        {
            _client = client;
            _client.CreateTopicIfNotExists(TopicNames.OpenPositionCommand);
        }
 
        public ValueTask PublishAsync(OpenPositionSbCommand contract)
        {
            var bytesToSend = contract.ServiceBusContractToByteArray();
            var task = _client.PublishAsync(TopicNames.OpenPositionCommand, bytesToSend, false);
            
            return new ValueTask(task);
        }
    }
}