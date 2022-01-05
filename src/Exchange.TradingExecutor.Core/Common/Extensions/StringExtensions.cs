namespace Exchange.TradingExecutor.Core.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string src)
        {
            return src == null || string.IsNullOrEmpty(src);
        }
    }
}