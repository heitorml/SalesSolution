using ErrorOr;
using Resales.Api.Shared.Responses;

namespace Resales.Api.Features.GetById
{
    public interface IGetResaleByIdFeature
    {
        Task<ErrorOr<ResalesResponse>> Execute(string id, CancellationToken cancellationToken);
    }
}
