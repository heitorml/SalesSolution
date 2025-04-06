using Dto.Resales.Responses;
using ErrorOr;

namespace Application.UseCases.Resales.GetByFilter
{
    public interface IGetAllResaleUseCase
    {
        Task<ErrorOr<List<ResalesResponseDto>>> Execute(CancellationToken cancellationToken);
    }
}
