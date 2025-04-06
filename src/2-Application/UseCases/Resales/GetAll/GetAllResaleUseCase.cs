using Application.Mapper;
using Application.UseCases.Resales.Create;
using Application.UseCases.Resales.GetByFilter;
using Domain.Entities;
using Dto.Resales;
using Dto.Resales.Responses;
using ErrorOr;
using Infrastructure.Repoistories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Resales.GetAll
{
    public class GetAllResaleUseCase : IGetAllResaleUseCase
    {
        private readonly ILogger<GetAllResaleUseCase> _logger;
        private readonly IRepository<Resale> _repository;

        public GetAllResaleUseCase(
            ILogger<GetAllResaleUseCase> logger,
            IRepository<Resale> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<ErrorOr<List<ResalesResponseDto>>> Execute(CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync(cancellationToken);
            return result.Select(x => ResalesMaper.ToResponseDto(x)).ToList();
        }
    }
}
