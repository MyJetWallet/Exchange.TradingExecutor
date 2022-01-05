using System.Runtime.Serialization;

namespace StatelessTradingExecutor.Grpc.Models
{
    [DataContract]
    public class ClosePositionGrpcCommand
    {
        [DataMember(Order = 1)] public string PositionId { get; set; }
        [DataMember(Order = 2)] public string AssetPairId { get; set; }
    }
}