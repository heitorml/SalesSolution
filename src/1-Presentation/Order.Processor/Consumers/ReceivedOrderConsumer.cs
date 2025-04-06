using Domain.Events.Orders;
using MassTransit;

namespace Orders.Worker.Consumers
{
    public class ReceivedOrderConsumer : IConsumer<ReceivedOrder>
    {
        private readonly ILogger<ReceivedOrderConsumer> _logger;

        public ReceivedOrderConsumer(
            ILogger<ReceivedOrderConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<ReceivedOrder> context)
        {
            _logger.LogInformation("Enviar e-mail de pedido criado com sucesso");
            return Task.CompletedTask;
        }
    }
}
