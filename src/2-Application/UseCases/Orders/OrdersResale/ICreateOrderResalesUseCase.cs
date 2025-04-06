using Dto.Orders.Reponses;
using Dto.Orders.Requests;
using ErrorOr;

namespace Application.UseCases.Orders.Receive
{
    public interface ICreateOrderResalesUseCase
    {
        Task<ErrorOr<OrderResponseDto>> Execute(OrderRequestDto orderDto, CancellationToken cancellationToken);
    }
}
