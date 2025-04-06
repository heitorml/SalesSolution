using Application.UseCases.Orders.Receive;
using Domain.Entities;
using Domain.Events.Orders;
using Dto.Orders.Requests;
using FluentAssertions;
using Infrastructure.Repoistories;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using Solution.Tests.Init;

namespace Solution.Tests._2_Application.UseCases.Orders
{
    public class CreateOrderResalesUseCaseTests
    {
        private readonly Mock<ILogger<CreateOrderResalesUseCase>> _loggerMock = new();
        private readonly Mock<IRepository<Order>> _repositoryMock = new();
        private readonly Mock<IBus> _busMock = new();
        private readonly CreateOrderResalesUseCase _useCase;

        public CreateOrderResalesUseCaseTests()
        {
            _useCase = new CreateOrderResalesUseCase(
                _loggerMock.Object,
                _repositoryMock.Object,
                _busMock.Object
            );
        }

        [Fact]
        public async Task Execute_Deve_CriarPedido_E_PublicarEvento()
        {
            // Arrange
            OrderRequestDto dto = OrdersInit.CreateDto();

            // Act
            var result = await _useCase.Execute(dto, CancellationToken.None);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Should().NotBeNull();
            result.Value.Price.Should().Be(400); // 2x100 + 1x200

            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Once);
            _busMock.Verify(b => b.Publish(It.IsAny<ReceivedOrder>(), It.IsAny<CancellationToken>()), Times.Once);
        }


    }
}
