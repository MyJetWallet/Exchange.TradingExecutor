using System.Threading.Tasks;

namespace Exchange.TradingExecutor.Core.Balance
{
    public interface IBalanceService
    {
        public Task ChangeAsync(ChangeBalanceModel model);
    }
}