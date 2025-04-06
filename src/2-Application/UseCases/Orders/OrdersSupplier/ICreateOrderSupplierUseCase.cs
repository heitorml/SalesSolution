using Dto.Orders.Reponses;
using ErrorOr;

namespace Application.UseCases.Orders.OrdersSupplier
{
    public interface ICreateOrderSupplierUseCase
    {
        Task<ErrorOr<OrderResponseDto>> Execute(string resalesId, CancellationToken cancellationToken);
    }
}
