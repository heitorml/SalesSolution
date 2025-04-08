using ErrorOr;
using Orders.Api.Shared.Responses;

namespace Orders.Api.Features.OrdersResale
{
    public interface ICreateOrderResalesFeature
    {
        Task<ErrorOr<OrderResponse>> Execute(CreateOrderResalesResquest orderDto, CancellationToken cancellationToken);
    }
}
