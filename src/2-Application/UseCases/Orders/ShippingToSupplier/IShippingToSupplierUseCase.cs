using Dto.Orders.Reponses;
using ErrorOr;

namespace Application.UseCases.Orders.ShippingToSupplier
{
    public interface IShippingToSupplierUseCase
    {
        Task<ErrorOr<OrderResponseDto>> Execute(string orderId, CancellationToken cancellationToken = default);

    }
}
