using MyYamlParser;

namespace Exchange.TradingExecutor.Core.Common.Models
{
    public class SettingsModel
    {
        public string AppName { get; set; } = "TradingExecutor";

        [YamlProperty("TradingExecutor.SpotServiceBusHostPort")]
        public string ServiceBusHostPort { get; set; }

        [YamlProperty("TradingExecutor.SeqServiceUrl")]
        public string SeqUrl { get; set; }

        [YamlProperty("TradingExecutor.BalanceServiceGrpcServiceUrl")]
        public string JetWalletExchangeGrpcServiceUrl { get; set; }
    }
}