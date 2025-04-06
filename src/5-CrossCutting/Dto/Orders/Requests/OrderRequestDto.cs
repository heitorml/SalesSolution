using Dto.OrderItems.Requests;
using Dto.Resales.Requests;

namespace Dto.Orders.Requests
{
    public class OrderRequestDto
    {
        public string ResaleId { get; set; }
        public ResalesRequestDto Resale { get; set; }
        public List<OrderItemsRequestDto> Items { get; set; }
    }
}
