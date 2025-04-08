using MassTransit;
using Orders.Api.Features.ShippingToSupplier;
using Orders.Worker.Events;
using System.Diagnostics;
using System.Text.Json;

namespace Orders.Worker.Consumers
{
    public class ShippingToSupplierConsumer : IConsumer<ReadyForShippingOrder>
    {
        private readonly ILogger<ShippingToSupplierConsumer> _logger;
        private readonly IShippingToSupplierFeature _shippingToSupplierUseCase;
        private static readonly ActivitySource _activitySource = new("ShippingToSupplier");

        public ShippingToSupplierConsumer(IShippingToSupplierFeature shippingToSupplierUseCase, ILogger<ShippingToSupplierConsumer> logger = null)
        {
            _shippingToSupplierUseCase = shippingToSupplierUseCase;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ReadyForShippingOrder> context)
        {
            using var activity = _activitySource.StartActivity("ShippingToSupplier");
            activity?.AddEvent(new ActivityEvent("ShippingToSupplier - Started"));
            activity?.SetTag("payload.request", context);

            try
            {
                var result = await _shippingToSupplierUseCase.Execute(context.Message.OrderId);
                activity?.SetTag("payload.response", JsonSerializer.Serialize(result.Value));
                activity?.AddEvent(new ActivityEvent("ReslesOrders - Finalized"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                activity?.AddEvent(new ActivityEvent("Exception"));
                activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                _logger.LogInformation("ReslesOrders - Finalized");
                throw;
            }
        }
    }
}
