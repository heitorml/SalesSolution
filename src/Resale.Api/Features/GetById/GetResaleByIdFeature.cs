using ErrorOr;
using Resales.Api.Entities;
using Resales.Api.Shared.Errors;
using Resales.Api.Shared.Mapper;
using Resales.Api.Shared.Repoistories;
using Resales.Api.Shared.Responses;

namespace Resales.Api.Features.GetById
{
    public class GetResaleByIdFeature : IGetResaleByIdFeature
    {
        private readonly ILogger<GetResaleByIdFeature> _logger;
        private readonly IRepository<Resale> _repository;

        public GetResaleByIdFeature(
            ILogger<GetResaleByIdFeature> logger,
            IRepository<Resale> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<ErrorOr<ResalesResponse>> Execute(string id, CancellationToken cancellationToken)
        {
            var resale = await _repository.GetByIdAsync(id, cancellationToken);

            if (resale == null)
                return ErrorCatalog.ResaleNotFound;

            return ResalesMaper.ToResponseDto(resale);
        }
    }
}
