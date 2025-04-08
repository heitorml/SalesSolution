using Orders.Api.Entities;
using Orders.Api.Features.OrdersResale;
using Orders.Api.Shared.Enums;
using Orders.Api.Shared.Responses;
using static Orders.Api.Shared.Responses.OrderResponse;

namespace Orders.Api.Shared.Mappers
{
    public static class OrdeMapper
    {
        public static Order ToEntity(CreateOrderResalesResquest dto)
            => new Order
            {
                Items = ToEntity(dto.Items),
                Resale = ToEntity(dto.Resale),
                CreatAt = DateTime.Now
            };


        public static Order ToEntityByStatus(CreateOrderResalesResquest dto, OrderStatus status)
            => new Order
            {
                Items = ToEntity(dto.Items),
                Resale = ToEntity(dto.Resale),
                Status = status,
                UpdatedAt = DateTime.Now
            };


        public static OrderResponse ToResponseDto(Order order)
           => new OrderResponse
           {
               Id = order.Id,
               Items = ToReponseDto(order.Items),
               Price = order.Price
           };


        public static Resale ToEntity(ResalesRequest dto)
         => new Resale
         {
             Name = dto.Name,
             Active = true,
             Cnpj = dto.Cnpj,
             ContactName = dto.ContactName,
             Email = dto.Email,
             FantasyName = dto.FantasyName,
             Phone = dto.Phone,
             Address = ToEntity(dto.Address),
             CreateAt = DateTime.UtcNow
         };

        public static ResalesResponseDto ToResponseDto(Resale resale)
          => new ResalesResponseDto
          {
              Id = resale.Id,
              Name = resale.Name,
              Cnpj = resale.Cnpj,
              ContactName = resale.ContactName,
              Email = resale.Email,
              FantasyName = resale.FantasyName,
              Phone = resale.Phone,
              CreateAt = resale.CreateAt,
              Active = resale.Active,
              Addresses = ToDto(resale.Address)
          };

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

        public static List<Address> ToEntity(List<AddressDto> dto)
          => dto.Select(x => new Address
          {
              Name = x.Street,
              ZipCode = x.ZipCode,
              City = x.City,
          }).ToList();

        public static List<AddressDto> ToDto(List<Address> dto)
            => dto.Select(x => new AddressDto
            {
                Street = x.Name,
                ZipCode = x.ZipCode,
                City = x.City,
            }).ToList();

        public static List<Address> ToEntity(AddressDto dto)
        {
            var listAddress = new List<Address>();
            listAddress.Add(new Address
            {
                Name = dto.Street,
                ZipCode = dto.ZipCode,
                City = dto.City,
            });
            return listAddress;
        }
    }
}
