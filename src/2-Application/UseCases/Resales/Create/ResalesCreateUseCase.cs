using Application.Mapper;
using Application.Validators;
using CrossCutting.Errors;
using Domain.Entities;
using Domain.Events.Resales;
using Dto.Resales;
using Dto.Resales.Requests;
using ErrorOr;
using Infrastructure.Repoistories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Resales.Create
{
    public class ResalesCreateUseCase : IResalesCreateUseCase
    {
        private readonly ILogger<ResalesCreateUseCase> _logger;
        private readonly IRepository<Resale> _repository;
        private readonly IBus _bus;

        public ResalesCreateUseCase(
            ILogger<ResalesCreateUseCase> logger,
            IRepository<Resale> repository,
            IBus bus)
        {
            _logger = logger;
            _repository = repository;
            _bus = bus;
        }

        public async Task<ErrorOr<string>> Execute(ResalesRequestDto dto, CancellationToken cancellationToken)
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
