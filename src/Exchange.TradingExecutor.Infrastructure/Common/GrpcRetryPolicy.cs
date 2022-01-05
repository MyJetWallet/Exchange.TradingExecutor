using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace Exchange.TradingExecutor.Infrastructure.Common
{
    public class GrpcRetryPolicy
    {
        private readonly AsyncRetryPolicy _retryPolicy;

        public GrpcRetryPolicy(ILogger<GrpcRetryPolicy> logger)
        {
            _retryPolicy = Policy
                .Handle<RpcException>()
                .WaitAndRetryAsync(3,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (exception, retryCount, context) =>
                    {
                        logger.LogWarning(
                            $"Failed grpc request {context.OperationKey}, retrying {retryCount}. {exception.Message} {exception.StackTrace}");
                    });
        }

        public Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> action)
        {
            return _retryPolicy.ExecuteAsync(action);
        }
    }
}