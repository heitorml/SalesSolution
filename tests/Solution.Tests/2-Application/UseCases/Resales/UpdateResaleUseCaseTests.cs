using Application.UseCases.Resales.Updade;
using CrossCutting.Errors;
using Domain.Entities;
using Dto.Address;
using Dto.Resales.Requests;
using FluentAssertions;
using Infrastructure.Repoistories;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;

namespace Solution.Tests._2_Application.UseCases.Resales
{
    public class UpdateResaleUseCaseTests
    {
        private readonly Mock<ILogger<UpdateResaleUseCase>> _loggerMock = new();
        private readonly Mock<IRepository<Resale>> _repositoryMock = new();
        private readonly Mock<IBus> _busMock = new();
        private readonly UpdateResaleUseCase _useCase;

        public UpdateResaleUseCaseTests()
        {
            _useCase = new UpdateResaleUseCase(_loggerMock.Object, _repositoryMock.Object, _busMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldUpdateResale_WhenExists()
        {
            // Arrange
            var id = "resale-1";
            var resale = new Resale
            {
                Id = id,
                Name = "Old Name",
                Cnpj = "00000000000100",
                Address = new List<Address> { new Address { Name = "Old St" } }
            };

            var dto = new ResaleUpdateRequestDto
            {
                Id = id,
                Name = "New Name",
                Cnpj = "12345678000199",
                Addresses = new List<AddressDto>
            {
                new AddressDto { Street = "New St" }
            }
            };

            _repositoryMock.Setup(r => r.GetByIdAsync(dto.Id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(resale);

            // Act
            var result = await _useCase.Execute(id, dto, CancellationToken.None);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Name.Should().Be("New Name");
            result.Value.Cnpj.Should().Be("12345678000199");

            _repositoryMock.Verify(r => r.UpdateAsync(id, resale, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Execute_ShouldReturnError_WhenResaleNotFound()
        {
            // Arrange
            var id = "resale-not-found";
            var dto = new ResaleUpdateRequestDto
            {
                Id = id,
                Name = "New Name",
                Cnpj = "12345678000199",
                Addresses = new List<AddressDto>
                {
                    new AddressDto { Street = "New St" }
                }
            };

            _repositoryMock.Setup(r => r.GetByIdAsync(dto.Id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync((Resale?)null);

            // Act
            var result = await _useCase.Execute(id, dto, CancellationToken.None);

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Code.Should().Be(ErrorCatalog.ResaleNotFound.Code);

            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<string>(), It.IsAny<Resale>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
