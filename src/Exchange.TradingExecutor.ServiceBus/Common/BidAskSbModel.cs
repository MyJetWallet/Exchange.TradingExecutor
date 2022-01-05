using System.Runtime.Serialization;

namespace StatelessTradingExecutor.ServiceBus.Common
{
    [DataContract]
    public class BidAskSbModel
    {
        [DataMember(Order = 1)] public string Id { get; set; }
    }
}