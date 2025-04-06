using Application.UseCases.Resales.Create;
using CrossCutting.Errors;
using Domain.Entities;
using Dto.Address;
using Dto.Resales.Requests;
using FluentAssertions;
using Infrastructure.Repoistories;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace Solution.Tests._2_Application.UseCases.Resales
{
    public class ResalesCreateUseCaseTests
    {
        private readonly Mock<IRepository<Resale>> _repositoryMock = new();
        private readonly Mock<IBus> _busMock = new();
        private readonly Mock<ILogger<ResalesCreateUseCase>> _loggerMock = new();
        private readonly ResalesCreateUseCase _useCase;

        public ResalesCreateUseCaseTests()
        {
            _useCase = new ResalesCreateUseCase(
                _loggerMock.Object,
                _repositoryMock.Object,
                _busMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnError_WhenResaleAlreadyExists()
        {
            // Arrange
            var dto = new ResalesRequestDto 
            { 
                Cnpj = "12345678900011", 
                Name = "Empresa X" ,
                Address = new AddressDto { Street = "Test", ZipCode = "test" }
            };
            var existingResale = new List<Resale> { new Resale { Cnpj = "12345678900011", Name = "Empresa X" } };

            _repositoryMock.Setup(r => r.FindAsync(It.IsAny<Expression<Func<Resale, bool>>>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(existingResale);

            // Act
            var result = await _useCase.Execute(dto, CancellationToken.None);

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Should().Be(ErrorCatalog.ResaleAlready);
        }

        [Fact]
        public async Task Execute_ShouldCreateResale_WhenItDoesNotExist()
        {
            // Arrange
            var dto = new ResalesRequestDto
            {
                Cnpj = "00000000000000",
                Name = "Nova Revenda",
                Address = new AddressDto
                {
                    ZipCode ="099898900",
                    Number = "s/n",
                    City =" 5432523"
                }
            };

            _repositoryMock.Setup(r => r.FindAsync(It.IsAny<Expression<Func<Resale, bool>>>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(new List<Resale>());

            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Resale>(), It.IsAny<CancellationToken>()))
                           .Callback<Resale, CancellationToken>((resale, _) => resale.Id = Guid.NewGuid().ToString())
                           .Returns(Task.CompletedTask);

            // Act
            var result = await _useCase.Execute(dto, CancellationToken.None);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().NotBeNullOrWhiteSpace();

            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Resale>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
