using Orders.Worker.Events;

namespace Orders.Worker.Features.OrderCancel
{
    public interface IOrderCancelFeature
    {
        Task Execute(CancelledOrderRequested orderDto, CancellationToken cancellationToken = default);
    }
}
