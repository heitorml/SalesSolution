namespace Orders.Api.Features.OrderCancel
{
    public interface IOrderCancelFeature
    {
        Task Execute(CancelledOrderRequested orderDto, CancellationToken cancellationToken = default);
    }
}
