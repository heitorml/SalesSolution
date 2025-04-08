using ErrorOr;
using MassTransit;
using Orders.Worker.Entities;
using Orders.Worker.Events;
using Orders.Worker.Shared.Enums;
using Orders.Worker.Shared.Errors;
using Orders.Worker.Shared.ExternalServices;
using Orders.Worker.Shared.Infrastructure.Repoistories;
using Orders.Worker.Shared.Mapper;
using Orders.Worker.Shared.Responses;

namespace Orders.Api.Features.ShippingToSupplier
{
    public class ShippingToSupplierFeature : IShippingToSupplierFeature
    {

        private readonly IRepository<Order> _repository;
        private readonly IExternalService _serviceExternal;
        private readonly IBus _bus;

        public ShippingToSupplierFeature(
            IRepository<Order> repository,
            IExternalService serviceExternal,
            IBus bus,
            IConfiguration configuration)
        {
            _repository = repository;
            _serviceExternal = serviceExternal;
            _bus = bus;
        }

        public async Task<ErrorOr<OrderResponse>> Execute(string orderId, CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(orderId, cancellationToken);
            if (order == null || order.Status != OrderStatus.ReadyForShipping)
                return Error.Failure(ErrorCatalog.OrderNotFound.Code,
                                      ErrorCatalog.OrderNotFound.Description);


            await _serviceExternal.Send(order, cancellationToken);


            order.Status = OrderStatus.ShippedToSupplier;
            await _repository.UpdateAsync(orderId, order, cancellationToken);

            await _bus.Publish(new OrderSentToSupplier
            {
                Id = orderId,
                Items = order.Items,
                Price = order.Price,
                Resale = order.Resale
            });

            return OrderMaper.ToResponseDto(order);
        }
    }
}
