using System.Diagnostics.CodeAnalysis;

namespace Resales.Api.Entities
{
    [ExcludeFromCodeCoverage]
    public class OrderItems
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
