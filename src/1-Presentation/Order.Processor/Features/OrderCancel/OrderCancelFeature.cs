using Application.UseCases.Orders.Receive;
using CrossCutting.Enums;
using Domain.Entities;
using Infrastructure.Repoistories;

namespace Orders.Api.Features.OrderCancel
{
    public class OrderCancelFeature : IOrderCancelFeature
    {
        private readonly IRepository<Order> _repository;
        private readonly ILogger<CreateOrderResalesUseCase> _logger;

        public OrderCancelFeature(
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

            //executar regras

            //enviar e-mail de notificação ao cliente 


            await _repository.UpdateAsync(orderDto.OrderId, order, cancellationToken);

            _logger.LogWarning("Order Cancelled");

            _logger.LogInformation("TO DO: Send Event CancelledOrder");

            Task.CompletedTask.Wait();
        }
    }
}
