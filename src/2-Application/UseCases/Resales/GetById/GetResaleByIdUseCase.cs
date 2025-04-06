using Application.Mapper;
using CrossCutting.Errors;
using Domain.Entities;
using Dto.Resales.Responses;
using ErrorOr;
using Infrastructure.Repoistories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Resales.GetById
{
    public class GetResaleByIdUseCase : IGetResaleByIdUseCase
    {
        private readonly ILogger<GetResaleByIdUseCase> _logger;
        private readonly IRepository<Resale> _repository;

        public GetResaleByIdUseCase(
            ILogger<GetResaleByIdUseCase> logger,
            IRepository<Resale> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<ErrorOr<ResalesResponseDto>> Execute(string id, CancellationToken cancellationToken)
        {
            var resale = await _repository.GetByIdAsync(id, cancellationToken);

            if (resale == null)
                return ErrorCatalog.ResaleNotFound;

            return ResalesMaper.ToResponseDto(resale);
        }
    }
}
