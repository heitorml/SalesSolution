namespace Orders.Api.Shared.Enums
{
    public enum OrderStatus
    {
        Received = 1,
        Merged,
        ReadyForShipping,
        ShippedToSupplier,
        Cancelled
    }
}
