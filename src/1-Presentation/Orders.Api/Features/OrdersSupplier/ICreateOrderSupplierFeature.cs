using ErrorOr;
using Orders.Api.Shared.Responses;

namespace Orders.Api.Features.OrdersSupplier
{
    public interface ICreateOrderSupplierFeature
    {
        Task<ErrorOr<OrderResponse>> Execute(string resalesId, CancellationToken cancellationToken);
    }
}
