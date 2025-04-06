using Application.Mapper;
using CrossCutting.Errors;
using Domain.Entities;
using Dto.Resales.Requests;
using Dto.Resales.Responses;
using ErrorOr;
using Infrastructure.Repoistories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Resales.Updade
{
    public class UpdateResaleUseCase : IUpdateResaleUseCase
    {
        private readonly ILogger<UpdateResaleUseCase> _logger;
        private readonly IRepository<Resale> _repository;
        private readonly IBus _bus;

        public UpdateResaleUseCase(
            ILogger<UpdateResaleUseCase> logger,
            IRepository<Resale> repository,
            IBus bus)
        {
            _logger = logger;
            _repository = repository;
            _bus = bus;
        }

        public async Task<ErrorOr<ResalesResponseDto>> Execute(
            string id,
            ResaleUpdateRequestDto dto,
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
