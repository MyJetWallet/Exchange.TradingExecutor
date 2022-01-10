namespace StatelessTradingExecutor.ServiceBus
{
    public static class TopicNames
    {
        public const string ClosedPosition = "exchange-closed-position";
        public const string ClosePositionCommand = "exchange-close-position";
        public const string OpenPositionCommand = "exchange-open-position";
    }
}