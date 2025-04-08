using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Orders.Api.Shared.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class MonitoringConfiguration
    {
        public static void AddMonitoringConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var resourceBuilder = ResourceBuilder.CreateDefault()
                 .AddService(configuration["ServiceName"])
                 .AddTelemetrySdk();

            services.AddSingleton(new ActivitySource(configuration["ServiceName"]));

            services.AddOpenTelemetry()
                .WithTracing(tracerProviderBuilder =>
                {
                    tracerProviderBuilder
                        .SetResourceBuilder(resourceBuilder)
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddSource(configuration["ServiceName"])
                        .AddOtlpExporter(opt =>
                        {
                            opt.Endpoint = new Uri(configuration["Monitoring:JeagerUrl"]); // OTLP collector
                            opt.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                        });
                });
        }
    }
}
