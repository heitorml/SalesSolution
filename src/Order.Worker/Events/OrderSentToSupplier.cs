using Orders.Worker.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Orders.Worker.Events
{
    [ExcludeFromCodeCoverage]
    public class OrderSentToSupplier
    {
        public string Id { get; set; }
        public Resale Resale { get; set; }
        public List<OrderItems> Items { get; set; }
        public decimal Price { get; set; }
    }
}
