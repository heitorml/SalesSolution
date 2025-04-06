using Application.UseCases.Resales.GetAll;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Repoistories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Solution.Tests._2_Application.UseCases.Resales
{

    public class GetAllResaleUseCaseTests
    {
        private readonly Mock<ILogger<GetAllResaleUseCase>> _loggerMock = new();
        private readonly Mock<IRepository<Resale>> _repositoryMock = new();
        private readonly GetAllResaleUseCase _useCase;

        public GetAllResaleUseCaseTests()
        {
            _useCase = new GetAllResaleUseCase(_loggerMock.Object, _repositoryMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnAllResalesMappedToDto()
        {
            // Arrange
            List<Resale> resaleList = CreateList();

            _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                           .ReturnsAsync(resaleList);

            // Act
            var result = await _useCase.Execute(CancellationToken.None);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().HaveCount(2);
            result.Value.First().Name.Should().Be("Revenda A");
            result.Value.Last().Cnpj.Should().Be("22345678000111");

            _repositoryMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        private static List<Resale> CreateList()
        {
            return new List<Resale>
            {
                new Resale { Id = "1", Name = "Revenda A", Cnpj = "12345678000100",
                    Address = new List<Address>
                    {
                         new Address {Name ="Test", ZipCode="test"},
                         new Address {Name ="Test2", ZipCode="test2"},
                    }
                },
                new Resale { Id = "2",Name = "Revenda B", Cnpj = "22345678000111",
                 Address = new List<Address>
                {
                     new Address {Name ="Test", ZipCode="test"},
                     new Address {Name ="Test2", ZipCode="test2"},
                }
}
            };
        }
    }
}
