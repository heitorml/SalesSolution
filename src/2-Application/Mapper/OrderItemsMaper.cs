using Domain.Entities;
using Dto.OrderItems.Requests;
using Dto.OrderItems.Responses;

namespace Application.Mapper
{
    public static class OrderItemsMaper
    {
        public static List<OrderItems> ToEntity(List<OrderItemsRequestDto> dto)
           => dto.Select(x => new OrderItems
           {
               Name = x.Name,
               Description = x.Description,
               Price = x.Price,
               Quantity = x.Quantity,
           }).ToList();


        public static List<OrderItemsResponseDto> ToReponseDto(List<OrderItems> items)
          => items.Select(x => new OrderItemsResponseDto
          {
              Name = x.Name,
              Description = x.Description,
              Price = x.Price,
              Quantity = x.Quantity,
          }).ToList();

    }
}
