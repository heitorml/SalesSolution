using Dto.OrderItems.Responses;

namespace Dto.Orders.Reponses
{
    public class OrderResponseDto
    {
        public string Id { get; set; }
        public List<OrderItemsResponseDto> Items { get; set; }
        public decimal Price { get; set; }

    }
}
