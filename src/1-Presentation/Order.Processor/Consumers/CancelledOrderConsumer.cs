using MassTransit;
using Orders.Worker.Events;
using Orders.Worker.Features.OrderCancel;

namespace Orders.Worker.Consumers
{
    public class CancelledOrderConsumer : IConsumer<CancelledOrderRequested>
    {
        private readonly ILogger<CancelledOrderConsumer> _logger;
        private readonly IOrderCancelFeature _useCase;

        public CancelledOrderConsumer(
            ILogger<CancelledOrderConsumer> logger,
            IOrderCancelFeature useCase)
        {
            _logger = logger;
            _useCase = useCase;
        }

        public async Task Consume(ConsumeContext<CancelledOrderRequested> context)
            => await _useCase.Execute(context.Message, CancellationToken.None);

    }
}
