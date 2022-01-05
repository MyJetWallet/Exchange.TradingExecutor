using System.Runtime.Serialization;

namespace StatelessTradingExecutor.Grpc.Models
{
    [DataContract]
    public class OpenPositionGrpcCommand
    {
        [DataMember(Order = 1)] public string WalletId { get; set; }
        [DataMember(Order = 2)] public string AssetPairId { get; set; }
        [DataMember(Order = 3)] public double InvestAmount { get; set; }
        [DataMember(Order = 4)] public string Leverage { get; set; }
        [DataMember(Order = 5)] public int PositionOperation { get; set; }
        [DataMember(Order = 6)] public double? TakeProfitInCurrency { get; set; }
        [DataMember(Order = 7)] public double? StopLossInCurrency { get; set; }
        [DataMember(Order = 8)] public double? TakeProfitRate { get; set; }
        [DataMember(Order = 9)] public double? StopLossRate { get; set; }
        [DataMember(Order = 10)] public long LastUpdateDate { get; set; }
        [DataMember(Order = 11)] public string ProcessId { get; set; }
        [DataMember(Order = 12)] public double Volume { get; set; }
        [DataMember(Order = 13)] public double Profit { get; set; }
        [DataMember(Order = 14)] public double StopOutPercent { get; set; }
    }
}