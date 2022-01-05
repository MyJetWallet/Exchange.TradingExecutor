using System.Threading.Tasks;
using DotNetCoreDecorators;
using Exchange.TradingExecutor.Core.Balance;
using Mapster;
using StatelessTradingExecutor.Grpc;
using StatelessTradingExecutor.Grpc.Models;
using StatelessTradingExecutor.ServiceBus.ClosePositionCommands;
using StatelessTradingExecutor.ServiceBus.OpenPositionCommands;

namespace Exchange.TradingExecutor.Grpc
{
    public class PositionsGrpcService : IPositionsGrpcService
    {
        private readonly IPublisher<OpenPositionSbCommand> _openPositionPublisher;
        private readonly IPublisher<ClosePositionSbCommand> _closePositionPublisher;
        private readonly IBalanceService _balanceService;

        public PositionsGrpcService(
            IPublisher<OpenPositionSbCommand> openPositionPublisher,
            IPublisher<ClosePositionSbCommand> closePositionPublisher,
            IBalanceService balanceService
        )
        {
            _openPositionPublisher = openPositionPublisher;
            _closePositionPublisher = closePositionPublisher;
            _balanceService = balanceService;
        }
        
        public async Task OpenPositionAsync(OpenPositionGrpcCommand request)
        {
            var sbModel = request.Adapt<OpenPositionSbCommand>();
            await _openPositionPublisher.PublishAsync(sbModel);
            var changeBalanceModel = new ChangeBalanceModel
            {
                Amount = sbModel.InvestAmount,
                ProcessId = sbModel.ProcessId,
                WalletId = sbModel.WalletId,
                AssetPairId = sbModel.AssetPairId,
                EventType = ChangeBalanceEventType.OpenPosition
            };
            await _balanceService.ChangeAsync(changeBalanceModel);
        }

        public async Task ClosePositionAsync(ClosePositionGrpcCommand request)
        {
            var sbModel = request.Adapt<ClosePositionSbCommand>();
            await _closePositionPublisher.PublishAsync(sbModel);
        }
    }
}