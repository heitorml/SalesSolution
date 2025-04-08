using ErrorOr;
using MassTransit;
using Resales.Api.Entities;
using Resales.Api.Shared.Errors;
using Resales.Api.Shared.Mapper;
using Resales.Api.Shared.Repoistories;
using Resales.Api.Shared.Requests;

namespace Resales.Api.Features.Create
{
    public class ResalesCreateFeature : IResalesCreateFeature
    {
        private readonly ILogger<ResalesCreateFeature> _logger;
        private readonly IRepository<Resale> _repository;
        private readonly IBus _bus;

        public ResalesCreateFeature(
            ILogger<ResalesCreateFeature> logger,
            IRepository<Resale> repository,
            IBus bus)
        {
            _logger = logger;
            _repository = repository;
            _bus = bus;
        }

        public async Task<ErrorOr<string>> Execute(ResalesCreateRequest dto, CancellationToken cancellationToken)
        {
            var resaleExists = await _repository.FindAsync(x => x.Cnpj.Equals(dto.Cnpj),
                cancellationToken);

            if (resaleExists.Any()) return ErrorCatalog.ResaleAlready;

            var newResale = ResalesMaper.ToEntity(dto);
            await _repository.AddAsync(newResale, cancellationToken);

            //  await _bus.Publish(new ResaleCreated(newResale), cancellationToken);

            return newResale.Id;
        }
    }
}
