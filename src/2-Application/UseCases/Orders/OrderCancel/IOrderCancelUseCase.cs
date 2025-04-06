using Domain.Events.Orders;

namespace Application.UseCases.Orders.OrderCancel
{
    public interface IOrderCancelUseCase
    {
        Task Execute(CancelledOrderRequested orderDto, CancellationToken cancellationToken = default);
    }
}
