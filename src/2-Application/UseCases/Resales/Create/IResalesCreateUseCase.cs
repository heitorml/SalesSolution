using Dto.Resales.Requests;
using ErrorOr;

namespace Application.UseCases.Resales.Create
{
    public interface IResalesCreateUseCase
    {
        Task<ErrorOr<string>> Execute(ResalesRequestDto dto, CancellationToken cancellationToken);
    }
}
