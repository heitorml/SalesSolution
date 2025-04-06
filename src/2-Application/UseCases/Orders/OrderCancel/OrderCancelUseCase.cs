using Application.UseCases.Orders.Receive;
using CrossCutting.Enums;
using Domain.Entities;
using Domain.Events.Orders;
using Infrastructure.Repoistories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Orders.OrderCancel
{
    public class OrderCancelUseCase : IOrderCancelUseCase
    {
        private readonly IRepository<Order> _repository;
        private readonly ILogger<CreateOrderResalesUseCase> _logger;

        public OrderCancelUseCase(
            IRepository<Order> repository,
            ILogger<CreateOrderResalesUseCase> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Execute(CancelledOrderRequested orderDto, CancellationToken cancellationToken = default)
        {
            var order = await _repository.GetByIdAsync(orderDto.OrderId, cancellationToken);
            order.Status = OrderStatus.Cancelled;

            await _repository.UpdateAsync(orderDto.OrderId, order, cancellationToken);

            _logger.LogWarning("Order Cancelled");

            _logger.LogInformation("TO DO: Send Event CancelledOrder");

            Task.CompletedTask.Wait();
        }
    }
}
