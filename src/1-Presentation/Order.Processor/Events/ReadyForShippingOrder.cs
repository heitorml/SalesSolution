using System.Diagnostics.CodeAnalysis;

namespace Orders.Worker.Events
{
    [ExcludeFromCodeCoverage]
    public class ReadyForShippingOrder
    {
        public string OrderId { get; set; }
    }
}
