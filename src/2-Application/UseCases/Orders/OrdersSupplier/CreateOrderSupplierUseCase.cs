using Application.Mapper;
using CrossCutting.Enums;
using CrossCutting.Errors;
using Domain.Entities;
using Domain.Events.Orders;
using Dto.Orders.Reponses;
using ErrorOr;
using Infrastructure.Repoistories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Orders.OrdersSupplier
{
    public class CreateOrderSupplierUseCase : ICreateOrderSupplierUseCase
    {

        private readonly ILogger<CreateOrderSupplierUseCase> _logger;
        private readonly IRepository<Order> _repository;
        private readonly IBus _bus;

        public CreateOrderSupplierUseCase(
            ILogger<CreateOrderSupplierUseCase> logger,
            IRepository<Order> repository,
            IBus bus)
        {
            _logger = logger;
            _repository = repository;
            _bus = bus;
        }

        public async Task<ErrorOr<OrderResponseDto>> Execute(
            string resalesId, 
            CancellationToken cancellationToken)
        {

            _logger.LogInformation("Busca de pedidos com status de Recebidos");
            var ordersByResale = await _repository.FindAsync(
                    x => x.Resale.Id == resalesId && 
                         x.Status == OrderStatus.Received,cancellationToken);
            
            if (!ordersByResale.Any())
                return Error.Failure(ErrorCatalog.ResaleNotFound.Code,
                                     ErrorCatalog.ResaleNotFound.Description);


            _logger.LogInformation("Junção dos pedidos");
            var items = ordersByResale
               .SelectMany(p => p.Items)
               .GroupBy(i => i.Name)
               .Select(g => new OrderItems
               {
                   Name = g.Key,
                   Quantity = g.Sum(x => x.Quantity),
                   Price = g.Sum(x => x.Price)
               }).ToList();


            _logger.LogInformation("Validação de quantidade de minima > 1000");
            if (items.Sum(i => i.Quantity) <= 1000)
                return Error.Failure(ErrorCatalog.MinimumQuantityNotReached.Code,
                                     ErrorCatalog.MinimumQuantityNotReached.Description);



            _logger.LogInformation("Criação do novo pedido com a junção de todos os pedidos recebidos");
            var newOrder = new Order()
                {
                    Items = items,
                    Resale = ordersByResale.First().Resale,
                    Status = OrderStatus.ReadyForShipping,
                    CreatAt = DateTime.Now                    
                };
            newOrder.Calculate();
            await _repository.AddAsync(newOrder, cancellationToken);


            _logger.LogInformation("Atualização dos status dos pedidos mesclados para Merged");
            foreach (var order in ordersByResale)
            {
                order.Status = OrderStatus.Merged;
                await _repository.UpdateAsync(order.Id, order, cancellationToken);
            }


            _logger.LogInformation("Envio de evento ReadyForShippingOrder");
            await _bus.Publish(new ReadyForShippingOrder { OrderId = newOrder.Id });

            return OrderMaper.ToResponseDto(newOrder);
        }
    }
}
