using ErrorOr;
using MassTransit;
using Orders.Api.Entities;
using Orders.Api.Events;
using Orders.Api.Shared.Enums;
using Orders.Api.Shared.Mappers;
using Orders.Api.Shared.Repoistories;
using Orders.Api.Shared.Responses;

namespace Orders.Api.Features.OrdersResale
{
    public class CreateOrderResalesFeture : ICreateOrderResalesFeature
    {
        private readonly ILogger<CreateOrderResalesFeture> _logger;
        private readonly IRepository<Order> _repository;
        private readonly IBus _bus;

        public CreateOrderResalesFeture(
            ILogger<CreateOrderResalesFeture> logger,
            IRepository<Order> repository,
            IBus bus)
        {
            _logger = logger;
            _repository = repository;
            _bus = bus;
        }

        public async Task<ErrorOr<OrderResponse>> Execute(
            CreateOrderResalesResquest orderRequest,
            CancellationToken cancellationToken)
        {
            var newOrder = OrdeMapper.ToEntityByStatus(orderRequest, OrderStatus.Received);

            newOrder.Calculate();
            newOrder.Resale.Id = orderRequest.ResaleId;

            await _repository.AddAsync(newOrder, cancellationToken);

            await _bus.Publish(new ReceivedOrder
            {
                Id = newOrder.Id,
                Resale = newOrder.Resale,
                Items = newOrder.Items,
                Price = newOrder.Price
            }, cancellationToken);

            return OrdeMapper.ToResponseDto(newOrder);
        }
    }
}
