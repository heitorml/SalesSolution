using Application.UseCases.Orders.OrderCancel;
using Domain.Events.Orders;
using MassTransit;

namespace Orders.Worker.Consumers
{
    public class CancelledOrderConsumer : IConsumer<CancelledOrderRequested>
    {
        private readonly ILogger<CancelledOrderConsumer> _logger;
        private readonly IOrderCancelUseCase _useCase;

        public CancelledOrderConsumer(
            ILogger<CancelledOrderConsumer> logger,
            IOrderCancelUseCase useCase)
        {
            _logger = logger;
            _useCase = useCase;
        }

        public async Task Consume(ConsumeContext<CancelledOrderRequested> context) 
            => await _useCase.Execute(context.Message, CancellationToken.None);

    }
}
