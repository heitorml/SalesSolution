using Orders.Api.Features.OrdersResale;

namespace Orders.Api.Shared.Responses
{
    public class OrderResponse
    {
        public string Id { get; set; }
        public List<OrderItemsResponseDto> Items { get; set; }
        public decimal Price { get; set; }


        public class OrderItemsResponseDto : OrderItemsDto
        {
        }

        public class ResalesResponseDto : ResalesDto
        {
            public string Id { get; set; }
            public List<AddressDto> Addresses { get; set; }
            public DateTime CreateAt { get; set; }
            public bool Active { get; set; }

        }

        public class OrderItemsDto
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }

    }
}
