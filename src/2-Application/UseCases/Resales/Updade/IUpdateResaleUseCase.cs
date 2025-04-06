using Dto.Resales.Requests;
using Dto.Resales.Responses;
using ErrorOr;

namespace Application.UseCases.Resales.Updade
{
    public interface IUpdateResaleUseCase
    {
        Task<ErrorOr<ResalesResponseDto>> Execute(
            string id, 
            ResaleUpdateRequestDto resale, 
            CancellationToken cancellationToken);
    }
}
