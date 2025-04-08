using ErrorOr;
using Resales.Api.Shared.Requests;

namespace Resales.Api.Features.Create
{
    public interface IResalesCreateFeature
    {
        Task<ErrorOr<string>> Execute(ResalesCreateRequest dto, CancellationToken cancellationToken);
    }
}
