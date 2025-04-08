using System.Diagnostics.CodeAnalysis;

namespace Orders.Api.Events
{
    [ExcludeFromCodeCoverage]
    public class ReadyForShippingOrder
    {
        public string OrderId { get; set; }
    }
}
