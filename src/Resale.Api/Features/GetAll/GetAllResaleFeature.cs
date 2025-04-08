using ErrorOr;
using Resales.Api.Entities;
using Resales.Api.Shared.Mapper;
using Resales.Api.Shared.Repoistories;
using Resales.Api.Shared.Responses;

namespace Resales.Api.Features.GetAll
{
    public class GetAllResaleFeature : IGetAllResaleFeature
    {
        private readonly ILogger<GetAllResaleFeature> _logger;
        private readonly IRepository<Resale> _repository;

        public GetAllResaleFeature(
            ILogger<GetAllResaleFeature> logger,
            IRepository<Resale> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<ErrorOr<List<ResalesResponse>>> Execute(CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync(cancellationToken);
            return result.Select(x => ResalesMaper.ToResponseDto(x)).ToList();
        }
    }
}
