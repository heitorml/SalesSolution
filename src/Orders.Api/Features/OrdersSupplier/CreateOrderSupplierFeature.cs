using ErrorOr;
using MassTransit;
using Orders.Api.Entities;
using Orders.Api.Events;
using Orders.Api.Shared.Enums;
using Orders.Api.Shared.Errors;
using Orders.Api.Shared.Mappers;
using Orders.Api.Shared.Repoistories;
using Orders.Api.Shared.Responses;


namespace Orders.Api.Features.OrdersSupplier
{
    public class CreateOrderSupplierFeature : ICreateOrderSupplierFeature
    {

        private readonly ILogger<CreateOrderSupplierFeature> _logger;
        private readonly IRepository<Order> _repository;
        private readonly IBus _bus;

        public CreateOrderSupplierFeature(
            ILogger<CreateOrderSupplierFeature> logger,
            IRepository<Order> repository,
            IBus bus)
        {
            _logger = logger;
            _repository = repository;
            _bus = bus;
        }

        public async Task<ErrorOr<OrderResponse>> Execute(
            string resalesId,
            CancellationToken cancellationToken)
        {

            _logger.LogInformation("Search for orders with Received status");
            var ordersByResale = await _repository.FindAsync(
                    x => x.Resale.Id == resalesId &&
                         x.Status == Shared.Enums.OrderStatus.Received, cancellationToken);

            if (!ordersByResale.Any())
                return Error.Failure(ErrorCatalog.OrderNotFound.Code,
                                     ErrorCatalog.OrderNotFound.Description);


            _logger.LogInformation("Merging orders");
            var items = ordersByResale
               .SelectMany(p => p.Items)
               .GroupBy(i => i.Name)
               .Select(g => new OrderItems
               {
                   Name = g.Key,
                   Quantity = g.Sum(x => x.Quantity),
                   Price = g.Sum(x => x.Price)
               }).ToList();


            _logger.LogInformation("Minimum quantity validation");
            if (items.Sum(i => i.Quantity) <= 1000)
                return Error.Failure(ErrorCatalog.MinimumQuantityNotReached.Code,
                                     ErrorCatalog.MinimumQuantityNotReached.Description);



            _logger.LogInformation("Creation of a new order by combining all received orders");
            var newOrder = new Order()
            {
                Items = items,
                Resale = ordersByResale.First().Resale,
                Status = OrderStatus.ReadyForShipping,
                CreatAt = DateTime.Now
            };
            newOrder.Calculate();
            await _repository.AddAsync(newOrder, cancellationToken);


            _logger.LogInformation("Updating the status of merged orders to Merged");
            foreach (var order in ordersByResale)
            {
                order.Status = OrderStatus.Merged;
                await _repository.UpdateAsync(order.Id, order, cancellationToken);
            }


            _logger.LogInformation("Sending ReadyForShippingOrder event");
            await _bus.Publish(new ReadyForShippingOrder { OrderId = newOrder.Id });

            return OrdeMapper.ToResponseDto(newOrder);
        }
    }
}
