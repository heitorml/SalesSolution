namespace Orders.Worker.Shared.Enums
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
