using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exchange.TradingExecutor.Core.Balance;
using Exchange.TradingExecutor.Infrastructure.Common;
using Service.Exchange.Balances.Grpc;
using Service.Exchange.Sdk.Messages;

namespace Exchange.TradingExecutor.Infrastructure.JetWallet
{
    public class JetWalletBalanceService : IBalanceService
    {
        private readonly IBalanceOperationService _balanceOperationService;
        private readonly GrpcRetryPolicy _grpcRetryPolicy;

        public JetWalletBalanceService(
            IBalanceOperationService balanceOperationService,
            GrpcRetryPolicy grpcRetryPolicy
        )
        {
            _balanceOperationService = balanceOperationService;
            _grpcRetryPolicy = grpcRetryPolicy;
        }

        public async Task ChangeAsync(ChangeBalanceModel model)
        {
            var instruction = new ExBalanceUpdateInstruction
            {
                Timestamp = DateTime.UtcNow,
                EventType = $"TradingExecutor {model.EventType}",
                OperationId = model.ProcessId,
                Updates = new List<ExBalanceUpdateInstruction.BalanceUpdate>
                {
                    new()
                    {
                        Amount = (decimal) model.Amount,
                        Number = 1,
                        ReserveAmount = 0,
                        AssetId = model.AssetPairId,
                        WalletId = model.WalletId
                    }
                }
            };
            var balanceUpdateResp = await _grpcRetryPolicy.ExecuteAsync(() =>
                _balanceOperationService.ProcessBalanceUpdate(instruction));

            if (balanceUpdateResp.Result != ExBalanceUpdate.BalanceUpdateResult.Ok)
            {
                throw new Exception($"Failed to UpdateBalance {balanceUpdateResp.Result}");
            }
        }
    }
}