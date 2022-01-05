using System.Runtime.Serialization;
using StatelessTradingExecutor.ServiceBus.Common;

namespace StatelessTradingExecutor.ServiceBus.ClosedPositions
{
    [DataContract]
    public class ClosedPositionSbModel
    {
        [DataMember(Order = 1)] public string Id { get; set; }
        [DataMember(Order = 2)] public string WalletId { get; set; }
        [DataMember(Order = 3)] public string AssetPairId { get; set; }
        [DataMember(Order = 4)] public double InvestAmount { get; set; }
        [DataMember(Order = 5)] public string Leverage { get; set; }
        [DataMember(Order = 6)] public long CreateDate { get; set; }
        [DataMember(Order = 7)] public int PositionOperation { get; set; }
        [DataMember(Order = 8)] public double? TakeProfitInCurrency { get; set; }
        [DataMember(Order = 9)] public double? StopLossInCurrency { get; set; }
        [DataMember(Order = 10)] public double? TakeProfitRate { get; set; }
        [DataMember(Order = 11)] public double? StopLossRate { get; set; }
        [DataMember(Order = 12)] public long LastUpdateDate { get; set; }
        [DataMember(Order = 13)] public string ProcessId { get; set; }
        [DataMember(Order = 14)] public double Volume { get; set; }
        [DataMember(Order = 15)] public double Profit { get; set; }
        [DataMember(Order = 16)] public double StopOutPercent { get; set; }
        [DataMember(Order = 17)] public BidAskSbModel CloseBidAsk { get; set; }
        [DataMember(Order = 18)] public double ClosePrice { get; set; }
        [DataMember(Order = 19)] public long CloseDate { get; set; }
        [DataMember(Order = 20)] public double BurnBonus { get; set; }
        [DataMember(Order = 21)] public int CloseReason { get; set; }
        [DataMember(Order = 22)] public double OpenPrice { get; set; }
        [DataMember(Order = 23)] public BidAskSbModel OpenBidAsk { get; set; }
        [DataMember(Order = 24)] public long OpenDate { get; set; }
    }
}