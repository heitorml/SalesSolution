using Dto.Resales.Responses;
using ErrorOr;

namespace Application.UseCases.Resales.GetById
{
    public interface IGetResaleByIdUseCase
    {
        Task<ErrorOr<ResalesResponseDto>> Execute(string id, CancellationToken cancellationToken);
    }
}
