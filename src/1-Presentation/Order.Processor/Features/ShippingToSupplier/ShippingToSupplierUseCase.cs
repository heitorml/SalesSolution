using MassTransit;
using ErrorOr;
using Orders.Api.Shared.Repoistories;
using Orders.Api.Entities;
using Orders.Api.Shared.Enums;
using Orders.Api.Shared.Errors;
using Orders.Api.ExternalServices;
using Orders.Api.Mapper;
using Orders.Api.Responses;
using Orders.Api.Events;

namespace Orders.Api.Features.ShippingToSupplier
{
    public class ShippingToSupplierUseCase : IShippingToSupplierUseCase
    {

        private readonly IRepository<Order> _repository;
        private readonly IExternalService _serviceExternal;
        private readonly IBus _bus;

        public ShippingToSupplierUseCase(
            IRepository<Order> repository,
            IExternalService serviceExternal,
            IBus bus,
            IConfiguration configuration)
        {
            _repository = repository;
            _serviceExternal = serviceExternal;
            _bus = bus;
        }

        public async Task<ErrorOr<OrderResponseDto>> Execute(string orderId, CancellationToken cancellationToken)
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
