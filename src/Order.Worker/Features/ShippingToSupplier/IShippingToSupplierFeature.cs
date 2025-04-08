using ErrorOr;
using Orders.Worker.Shared.Responses;

namespace Orders.Api.Features.ShippingToSupplier
{
    public interface IShippingToSupplierFeature
    {
        Task<ErrorOr<OrderResponse>> Execute(string orderId, CancellationToken cancellationToken = default);

    }
}
