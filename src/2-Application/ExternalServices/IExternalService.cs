using Domain.Entities;
using Refit;

namespace Application.ExternalServices
{
    public interface IExternalService
    {
        [Post("/order")]
        public Task<bool> Send(Order orders,CancellationToken cancellationToken);
    }
}
