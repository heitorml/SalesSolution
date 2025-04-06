using Application.UseCases.Resales.GetById;
using CrossCutting.Errors;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Repoistories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Solution.Tests._2_Application.UseCases.Resales
{
    public class GetResaleByIdUseCaseTests
    {
        private readonly Mock<ILogger<GetResaleByIdUseCase>> _loggerMock = new();
        private readonly Mock<IRepository<Resale>> _repositoryMock = new();
        private readonly GetResaleByIdUseCase _useCase;

        public GetResaleByIdUseCaseTests()
        {
            _useCase = new GetResaleByIdUseCase(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnResaleDto_WhenResaleExists()
        {
            // Arrange
            var resaleId = "resale-1";
            var resale = new Resale
            {
                Id = resaleId,
                Name = "Revenda Teste",
                Cnpj = "12345678000199",
                Address = new List<Address> { new Address { ZipCode = "12345678900011", Name = "Empresa X" } }
            };

            _repositoryMock.Setup(r => r.GetByIdAsync(resaleId, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(resale);

            // Act
            var result = await _useCase.Execute(resaleId, CancellationToken.None);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Id.Should().Be(resaleId);
            result.Value.Name.Should().Be("Revenda Teste");
            result.Value.Cnpj.Should().Be("12345678000199");

            _repositoryMock.Verify(r => r.GetByIdAsync(resaleId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Execute_ShouldReturnError_WhenResaleDoesNotExist()
        {
            // Arrange
            var resaleId = "not-found-id";

            _repositoryMock.Setup(r => r.GetByIdAsync(resaleId, It.IsAny<CancellationToken>()))
                           .ReturnsAsync((Resale)null);

            // Act
            var result = await _useCase.Execute(resaleId, CancellationToken.None);

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Code.Should().Be(ErrorCatalog.ResaleNotFound.Code);
            result.FirstError.Description.Should().Be(ErrorCatalog.ResaleNotFound.Description);

            _repositoryMock.Verify(r => r.GetByIdAsync(resaleId, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
