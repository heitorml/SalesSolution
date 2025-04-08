using Orders.Worker.Shared.Requests;

namespace Orders.Worker.Shared.Responses
{
    public class OrderResponse
    {
        public string Id { get; set; }
        public List<OrderItemsResponseDto> Items { get; set; }
        public decimal Price { get; set; }


        public class OrderItemsResponseDto : OrderItemsDto
        {
        }

        public class ResalesResponseDto : ResalesRequest
        {
            public string Id { get; set; }
            public List<AddressRequest> Addresses { get; set; }
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
