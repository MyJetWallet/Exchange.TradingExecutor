namespace Exchange.TradingExecutor.Core.Balance
{
    public class ChangeBalanceModel
    {
        public string ProcessId { get; set; }
        public double Amount { get; set; }
        public string AssetPairId { get; set; }
        public string WalletId { get; set; }
        public ChangeBalanceEventType EventType { get; set; }
    }
}