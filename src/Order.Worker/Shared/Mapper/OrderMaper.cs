using Orders.Worker.Entities;
using Orders.Worker.Shared.Enums;
using Orders.Worker.Shared.Requests;
using Orders.Worker.Shared.Responses;

namespace Orders.Worker.Shared.Mapper
{
    public static class OrderMaper
    {
        public static Order ToEntity(OrderRequestDto dto)
            => new Order
            {
                Items = OrderItemsMaper.ToEntity(dto.Items),
                Resale = ResalesMaper.ToEntity(dto.Resale),
                CreatAt = DateTime.Now
            };


        public static Order ToEntityByStatus(OrderRequestDto dto, OrderStatus status)
            => new Order
            {
                Items = OrderItemsMaper.ToEntity(dto.Items),
                Resale = ResalesMaper.ToEntity(dto.Resale),
                Status = status,
                UpdatedAt = DateTime.Now
            };


        public static OrderResponse ToResponseDto(Order order)
           => new OrderResponse
           {
               Id = order.Id,
            //   Items = OrderItemsMaper.ToReponseDto(order.Items),
               Price = order.Price
           };
    }
}
