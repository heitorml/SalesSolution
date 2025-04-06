using Application.UseCases.Orders.OrderCancel;
using Domain.Events.Orders;
using MassTransit;
using System.Diagnostics.CodeAnalysis;

namespace Orders.Worker.Consumers
{
    [ExcludeFromCodeCoverage]
    public class ShippingToSupplierFaultConsumer : IConsumer<Fault<ReadyForShippingOrder>>
    {
        private readonly ILogger<ShippingToSupplierFaultConsumer> _logger;
        private readonly IOrderCancelUseCase _useCase;

        public ShippingToSupplierFaultConsumer(
            ILogger<ShippingToSupplierFaultConsumer> 
            logger, IOrderCancelUseCase useCase = null)
        {
            _logger = logger;
            _useCase = useCase;
        }

        public async Task Consume(ConsumeContext<Fault<ReadyForShippingOrder>> context)
        {
            await _useCase.Execute(new CancelledOrderRequested { 
                OrderId = context.Message.Message.OrderId
            });
            _logger.LogWarning("Order Cancelled");
        }
    }
}
