using Orders.Api.Entities;
using Refit;

namespace Orders.Api.Shared.ExternalServices
{
    public interface IExternalService
    {
        [Post("/order")]
        public Task<bool> Send(Order orders, CancellationToken cancellationToken);
    }
}
