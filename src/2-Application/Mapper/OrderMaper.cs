using CrossCutting.Enums;
using Domain.Entities;
using Dto.Orders.Reponses;
using Dto.Orders.Requests;

namespace Application.Mapper
{
    public static class OrderMaper
    {
        public static Order ToEntity(OrderRequestDto dto)
            => new Order
            {
                Items = OrderItemsMaper.ToEntity(dto.Items),
                Resale = ResalesMaper.ToEntity(dto.Resale),
                CreatAt =  DateTime.Now
            };

        
        public static Order ToEntityByStatus(OrderRequestDto dto, OrderStatus status)
            => new Order
            {
                Items = OrderItemsMaper.ToEntity(dto.Items),
                Resale = ResalesMaper.ToEntity(dto.Resale),
                Status = status,
                UpdatedAt = DateTime.Now
            };


        public static OrderResponseDto ToResponseDto(Order order)
           => new OrderResponseDto
           {
               Id = order.Id,
               Items = OrderItemsMaper.ToReponseDto(order.Items),
               Price = order.Price
           };
    }
}
