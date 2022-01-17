using Destructurama;
using Exchange.TradingExecutor.Core.Balance;
using Exchange.TradingExecutor.Core.Common.Models;
using Exchange.TradingExecutor.Infrastructure.Common;
using Exchange.TradingExecutor.Infrastructure.Spot;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Service;
using Serilog;
using Service.Exchange.Balances.Client;
using Service.Exchange.Balances.Grpc;

namespace Exchange.TradingExecutor.Infrastructure
{
    public static class ServiceBinder
    {
        public static void AddInfrastructure(this IServiceCollection services, SettingsModel settings)
        {
            var loggerFactory = LogConfigurator.Configure(settings.AppName, settings.SeqUrl);
            services.AddSingleton(loggerFactory);
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            services.AddGrpcServices(settings);
            services.AddRepositories(settings);
            services.AddServices(settings);
        }

        private static void AddRepositories(this IServiceCollection services, SettingsModel settings)
        {
        }

        private static void AddServices(this IServiceCollection services, SettingsModel settings)
        {
            services.AddSingleton<IBalanceService, SpotBalanceService>();
        }

        private static void AddGrpcServices(this IServiceCollection services, SettingsModel settings)
        {
            services.AddSingleton<GrpcRetryPolicy>();
            var factory = new ExchangeBalancesClientFactory(settings.SpotBalanceOperationGrpcServiceUrl);
            services.AddSingleton<IBalanceOperationService>(factory.GetBalanceOperationService());
        }
    }
}