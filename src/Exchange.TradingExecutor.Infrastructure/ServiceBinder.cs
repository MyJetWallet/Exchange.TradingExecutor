using Destructurama;
using Exchange.TradingExecutor.Core.Balance;
using Exchange.TradingExecutor.Core.Common.Models;
using Exchange.TradingExecutor.Infrastructure.Common;
using Exchange.TradingExecutor.Infrastructure.JetWallet;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Service.Exchange.Balances.Client;
using Service.Exchange.Balances.Grpc;

namespace Exchange.TradingExecutor.Infrastructure
{
    public static class ServiceBinder
    {
        public static void AddInfrastructure(this IServiceCollection services, SettingsModel settings)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("AppName", settings.AppName)
                .WriteTo.Seq(settings.SeqUrl)
                .WriteTo.Console()
                .CreateLogger();
            services.AddLogging(s => s.AddSerilog());

            services.AddGrpcServices(settings);
            services.AddRepositories(settings);
            services.AddServices(settings);
        }

        private static void AddRepositories(this IServiceCollection services, SettingsModel settings)
        {
        }

        private static void AddServices(this IServiceCollection services, SettingsModel settings)
        {
            services.AddSingleton<IBalanceService, JetWalletBalanceService>();
        }

        private static void AddGrpcServices(this IServiceCollection services, SettingsModel settings)
        {
            services.AddSingleton<GrpcRetryPolicy>();
            var factory = new ExchangeBalancesClientFactory(settings.JetWalletExchangeGrpcServiceUrl);
            services.AddSingleton<IBalanceOperationService>(factory.GetBalanceOperationService());
        }
    }
}