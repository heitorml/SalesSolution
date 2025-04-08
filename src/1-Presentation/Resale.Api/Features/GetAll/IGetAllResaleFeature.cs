using ErrorOr;
using Resales.Api.Shared.Responses;

namespace Resales.Api.Features.GetAll
{
    public interface IGetAllResaleFeature
    {
        Task<ErrorOr<List<ResalesResponse>>> Execute(CancellationToken cancellationToken);
    }
}
