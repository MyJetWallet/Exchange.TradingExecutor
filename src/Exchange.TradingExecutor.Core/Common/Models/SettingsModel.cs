using MyYamlParser;

namespace Exchange.TradingExecutor.Core.Common.Models
{
    public class SettingsModel
    {
        public string AppName { get; set; } = "TradingExecutor";

        [YamlProperty("TradingExecutor.ServiceBusHostPort")]
        public string ServiceBusHostPort { get; set; }

        [YamlProperty("TradingExecutor.SeqUrl")]
        public string SeqUrl { get; set; }

        [YamlProperty("TradingExecutor.JetWalletExchangeGrpcServiceUrl")]
        public string JetWalletExchangeGrpcServiceUrl { get; set; }
    }
}