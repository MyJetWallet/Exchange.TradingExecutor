namespace Exchange.TradingExecutor.Core.Common.Enums
{
    public enum ClosePositionReason
    {
        None = 0,
        ClientCommand = 1,
        StopOut = 2,
        TakeProfit = 3,
        StopLoss = 4,
        Canceled = 5,
        AdminAction = 6,
    }
}