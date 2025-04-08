using ErrorOr;
using Orders.Api.Responses;

namespace Orders.Api.Features.ShippingToSupplier
{
    public interface IShippingToSupplierUseCase
    {
        Task<ErrorOr<OrderResponseDto>> Execute(string orderId, CancellationToken cancellationToken = default);

    }
}
