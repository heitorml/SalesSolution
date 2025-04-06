using Application.Mapper;
using CrossCutting.Enums;
using Domain.Entities;
using Domain.Events.Orders;
using Dto.Orders.Reponses;
using Dto.Orders.Requests;
using ErrorOr;
using Infrastructure.Repoistories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Orders.Receive
{
    public class CreateOrderResalesUseCase : ICreateOrderResalesUseCase
    {
        private readonly ILogger<CreateOrderResalesUseCase> _logger;
        private readonly IRepository<Order> _repository;
        private readonly IBus _bus;

        public CreateOrderResalesUseCase(
            ILogger<CreateOrderResalesUseCase> logger,
            IRepository<Order> repository,
            IBus bus)
        {
            _logger = logger;
            _repository = repository;
            _bus = bus;
        }

        public async Task<ErrorOr<OrderResponseDto>> Execute(
            OrderRequestDto orderDto, 
            CancellationToken cancellationToken)
        {
            var newOrder = OrderMaper.ToEntityByStatus(orderDto, OrderStatus.Received);
           
            newOrder.Calculate();
            newOrder.Resale.Id = orderDto.ResaleId;
            
            await _repository.AddAsync(newOrder, cancellationToken);

            await _bus.Publish(new ReceivedOrder(newOrder), cancellationToken);

            return OrderMaper.ToResponseDto(newOrder);
        }
    }
}
