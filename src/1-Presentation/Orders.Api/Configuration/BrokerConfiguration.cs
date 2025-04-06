using MassTransit;
using System.Diagnostics.CodeAnalysis;

namespace Orders.Api.Configuration
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
                    cfg.Host(configuration["RabbitMq:Host"], "/", h =>
                    {
                        h.Username(configuration["RabbitMq:Username"]);//guest
                        h.Password(configuration["RabbitMq:Password"]);//guest
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}
