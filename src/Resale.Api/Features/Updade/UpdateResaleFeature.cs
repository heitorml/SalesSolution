using ErrorOr;
using MassTransit;
using Resales.Api.Entities;
using Resales.Api.Shared.Errors;
using Resales.Api.Shared.Mapper;
using Resales.Api.Shared.Repoistories;
using Resales.Api.Shared.Requests;
using Resales.Api.Shared.Responses;

namespace Resales.Api.Features.Updade
{
    public class UpdateResaleFeature : IUpdateResaleFeature
    {
        private readonly ILogger<UpdateResaleFeature> _logger;
        private readonly IRepository<Resale> _repository;
        private readonly IBus _bus;

        public UpdateResaleFeature(
            ILogger<UpdateResaleFeature> logger,
            IRepository<Resale> repository,
            IBus bus)
        {
            _logger = logger;
            _repository = repository;
            _bus = bus;
        }

        public async Task<ErrorOr<ResalesResponse>> Execute(
            string id,
            ResaleUpdateRequest dto,
            CancellationToken cancellationToken)
        {
            var resale = await _repository.GetByIdAsync(dto.Id);

            if (resale == null)
                return ErrorCatalog.ResaleNotFound;

            resale.Name = dto.Name;
            resale.Cnpj = dto.Cnpj;
            resale.Address = AddressMaper.ToEntity(dto.Addresses);

            await _repository.UpdateAsync(id, resale, cancellationToken);

            //  await _bus.Publish(new ResaleUpdated(resale));

            return ResalesMaper.ToResponseDto(resale);
        }
    }
}
