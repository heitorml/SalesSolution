using CrossCutting;
using CrossCutting.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Order : IEntity
    {
        public string Id { get; set; }
        public Resale Resale { get; set; }
        public List<OrderItems> Items { get; set; }
        public decimal Price { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public void Calculate() => Price = Items.Sum(item => item.Quantity * item.Price);

        public Order() {}
    }
}
