using MassTransit;
using System.Diagnostics.CodeAnalysis;

namespace Orders.Worker.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class BrokerConfiguration
    {
        public static void AddBrokerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                x.AddConsumers(typeof(Program).Assembly);

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration["RabbitMq:Host"], "/", h => {
                        h.Username(configuration["RabbitMq:Username"]);
                        h.Password(configuration["RabbitMq:Password"]);
                    });

                    cfg.UseMessageRetry(retry =>
                    {
                        retry.Exponential(
                            retryLimit: 5,                                // Tenta até 5 vezes
                            minInterval: TimeSpan.FromSeconds(1),         // Começa com 1s
                            maxInterval: TimeSpan.FromSeconds(30),        // Máximo 30s
                            intervalDelta: TimeSpan.FromSeconds(3));      // Jitter (aleatoriedade)
                    });

                    cfg.UseCircuitBreaker(cb =>
                    {
                        cb.TrackingPeriod = TimeSpan.FromMinutes(1);      // Janela de monitoramento
                        cb.TripThreshold = 1;                           // 15% de falhas dispara o break
                        cb.ActiveThreshold = 10;                           // Ao menos 10 mensagens processadas antes de ativar
                        cb.ResetInterval = TimeSpan.FromMinutes(5);        // Tenta reiniciar após 5 minutos
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}
