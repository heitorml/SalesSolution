using Orders.Worker.Entities;
using Refit;

namespace Orders.Worker.Shared.ExternalServices
{
    public interface IExternalService
    {
        [Post("/order")]
        public Task<bool> Send(Order orders, CancellationToken cancellationToken);
    }
}
