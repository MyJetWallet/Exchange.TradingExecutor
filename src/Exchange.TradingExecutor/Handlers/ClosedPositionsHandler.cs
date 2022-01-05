using System;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using Exchange.TradingExecutor.Core.Balance;
using Exchange.TradingExecutor.Core.Common.Interfaces;
using Microsoft.Extensions.Logging;
using StatelessTradingExecutor.ServiceBus.ClosedPositions;

namespace Exchange.TradingExecutor.Handlers
{
    public class ClosedPositionsHandler : IStartableService
    {
        private readonly ILogger<ClosedPositionsHandler> _logger;
        private readonly ISubscriber<ClosedPositionSbModel> _subscriber;
        private readonly IBalanceService _balanceService;

        public ClosedPositionsHandler(
            ILogger<ClosedPositionsHandler> logger,
            ISubscriber<ClosedPositionSbModel> subscriber,
            IBalanceService balanceService
        )
        {
            _logger = logger;
            _subscriber = subscriber;
            _balanceService = balanceService;
        }

        public void Start()
        {
            _subscriber.Subscribe(HandleAsync);
        }

        private ValueTask HandleAsync(ClosedPositionSbModel sbModel)
        {
            _ = Task.Run(async () =>
            {
                try
                {
                    var model = new ChangeBalanceModel
                    {
                        Amount = sbModel.Profit,
                        ProcessId = sbModel.ProcessId,
                        WalletId = sbModel.WalletId,
                        AssetPairId = sbModel.AssetPairId,
                        EventType = ChangeBalanceEventType.ClosedPosition
                    };
                    await _balanceService.ChangeAsync(model);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to handle ClosedPositionSbModel {@sbModel}", sbModel);
                }
            });

            return ValueTask.CompletedTask;
        }
    }
}