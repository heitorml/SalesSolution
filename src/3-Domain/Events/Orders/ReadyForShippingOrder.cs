using System.Diagnostics.CodeAnalysis;

namespace Domain.Events.Orders
{
    [ExcludeFromCodeCoverage]
    public class ReadyForShippingOrder
    {
        public string OrderId { get; set; }
    }
}
