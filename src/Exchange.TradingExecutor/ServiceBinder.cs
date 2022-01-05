using DotNetCoreDecorators;
using Exchange.TradingExecutor.Core;
using Exchange.TradingExecutor.Core.Common.Extensions;
using Exchange.TradingExecutor.Core.Common.Interfaces;
using Exchange.TradingExecutor.Core.Common.Models;
using Exchange.TradingExecutor.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MyServiceBus.Abstractions;
using MyServiceBus.TcpClient;
using Serilog;
using StatelessTradingExecutor.ServiceBus.ClosedPositions;
using StatelessTradingExecutor.ServiceBus.ClosePositionCommands;
using StatelessTradingExecutor.ServiceBus.OpenPositionCommands;

namespace Exchange.TradingExecutor
{
    public static class ServiceBinder
    {
        public static void AddServices(this IServiceCollection services, SettingsModel settings)
        { 
            services.AddMarkedBy<IStartableService>(ServiceLifetime.Singleton, typeof(ServiceBinder).Assembly);
            services.AddServiceBus(settings);
            services.AddCore(settings);
            services.AddInfrastructure(settings);
        }
        
        private static void AddServiceBus(this IServiceCollection services, SettingsModel settings)
        {
            var tcpServiceBus = new MyServiceBusTcpClient(
                () => settings.ServiceBusHostPort, settings.AppName);
            services.AddSingleton(tcpServiceBus);
            services.AddSingleton<IPublisher<OpenPositionSbCommand>>(new OpenPositionCommandSbPublisher(tcpServiceBus));
            services.AddSingleton<IPublisher<ClosePositionSbCommand>>(new ClosePositionCommandSbPublisher(tcpServiceBus));
            services.AddSingleton<ISubscriber<ClosedPositionSbModel>>(
                new ClosedPositionSbSubscriber(tcpServiceBus, settings.AppName, TopicQueueType.DeleteOnDisconnect, false));
        }
    }
}