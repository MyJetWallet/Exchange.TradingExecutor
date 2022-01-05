using System.Collections.Generic;
using System.Reflection;
using Exchange.TradingExecutor.Core.Common.Interfaces;
using Exchange.TradingExecutor.Core.Common.Models;
using Exchange.TradingExecutor.Grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyJetWallet.Sdk.GrpcSchema;
using MyServiceBus.TcpClient;
using MySettingsReader;
using Prometheus;
using ProtoBuf.Grpc.Server;
using SimpleTrading.BaseMetrics;
using SimpleTrading.ServiceStatusReporterConnector;
using StatelessTradingExecutor.Grpc;

namespace Exchange.TradingExecutor
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private static SettingsModel _settings;

        public Startup(IConfiguration configuration)
        {
            _settings = SettingsReader.GetSettings<SettingsModel>(".simple-trading");
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
#if(DEBUG)
            _settings.AppName += "-local";
#endif
            
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddSingleton(_settings);
            services.AddServices(_settings);

            services.AddCodeFirstGrpc(option =>
            {
                option.BindMetricsInterceptors();
                option.Interceptors.Add<LoggerInterceptor>();
            });
        }

        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            MyServiceBusTcpClient tcpClient,
            IEnumerable<IStartableService> startableServices)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.BindIsAlive();
            app.BindMetricsMiddleware();
            app.BindServicesTree(Assembly.GetExecutingAssembly());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcSchema<PositionsGrpcService, IPositionsGrpcService>();
                endpoints.MapGrpcSchemaRegistry();
                endpoints.MapMetrics();
            });
            
            tcpClient.Start();
            foreach (var service in startableServices)
            {
                service.Start();
            }
        }
    }
}