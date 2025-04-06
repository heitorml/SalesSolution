using Application.ExternalServices;
using Application.Mapper;
using CrossCutting.Enums;
using CrossCutting.Errors;
using Domain.Entities;
using Domain.Events.Orders;
using Dto.Orders.Reponses;
using ErrorOr;
using Infrastructure.Repoistories;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace Application.UseCases.Orders.ShippingToSupplier
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
