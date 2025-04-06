using Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Events.Orders
{
    [ExcludeFromCodeCoverage]
    public class ReceivedOrder
    {
        public string Id { get; set; }
        public Resale Resale { get; set; }
        public List<OrderItems> Items { get; set; }
        public decimal Price { get; set; }

        public ReceivedOrder(Order order)
        {
            Id = order.Id;
            Resale = order.Resale;
            Items = order.Items;
            Price = order.Price;
        }
    }
}
