using ErrorOr;
using Resales.Api.Shared.Requests;
using Resales.Api.Shared.Responses;

namespace Resales.Api.Features.Updade
{
    public interface IUpdateResaleFeature
    {
        Task<ErrorOr<ResalesResponse>> Execute(
            string id,
            ResaleUpdateRequest resale,
            CancellationToken cancellationToken);
    }
}
