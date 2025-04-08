namespace Orders.Api.Features.OrdersResale
{
    public class CreateOrderResalesResquest
    {
        public string ResaleId { get; set; }
        public ResalesRequest Resale { get; set; }
        public List<OrderItemsRequestDto> Items { get; set; }
    }

    public class ResalesRequest : ResalesDto
    {
        public AddressDto Address { get; set; }
    }

    public class ResalesDto
    {
        public string Name { get; set; }
        public string FantasyName { get; set; }
        public string Phone { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public string Cnpj { get; set; }
    }

    public class OrderItemsDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class AddressDto
    {
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
    }

    public class OrderItemsRequestDto : OrderItemsDto
    {
    }
}
