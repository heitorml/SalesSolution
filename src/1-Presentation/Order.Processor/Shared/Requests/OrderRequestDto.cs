namespace Orders.Worker.Shared.Requests
{
    public class OrderRequestDto
    {
        public string ResaleId { get; set; }
        public ResalesRequest Resale { get; set; }
        public List<OrderItemsRequestDto> Items { get; set; }
    }
}
