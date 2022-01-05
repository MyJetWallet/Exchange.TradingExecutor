using System.Runtime.Serialization;

namespace StatelessTradingExecutor.ServiceBus.ClosePositionCommands
{
    [DataContract]
    public class ClosePositionSbCommand
    {
        [DataMember(Order = 1)] public string PositionId { get; set; }
        [DataMember(Order = 2)] public string AssetPairId { get; set; }
    }
}