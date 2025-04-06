using Application.UseCases.Orders.OrderCancel;
using Application.UseCases.Orders.Receive;
using CrossCutting.Enums;
using Domain.Entities;
using Domain.Events.Orders;
using Infrastructure.Repoistories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Solution.Tests._2_Application.UseCases.Orders
{
    public class OrderCancelUseCaseTests
    {
        private readonly Mock<IRepository<Order>> _repositoryMock;
        private readonly Mock<ILogger<CreateOrderResalesUseCase>> _loggerMock;
        private readonly OrderCancelUseCase _useCase;

        public OrderCancelUseCaseTests()
        {
            _repositoryMock = new Mock<IRepository<Order>>();
            _loggerMock = new Mock<ILogger<CreateOrderResalesUseCase>>();
            _useCase = new OrderCancelUseCase(_repositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Execute_Should_Update_OrderStatus_To_Cancelled()
        {
            // Arrange
            var orderId = "order-123";
            var existingOrder = new Order { Id = orderId, Status = OrderStatus.Cancelled };

            var request = new CancelledOrderRequested { OrderId = orderId };

            _repositoryMock
                .Setup(repo => repo.GetByIdAsync(orderId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingOrder);

            _repositoryMock
                .Setup(repo => repo.UpdateAsync(orderId, It.IsAny<Order>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            await _useCase.Execute(request);

            // Assert
            Assert.Equal(OrderStatus.Cancelled, existingOrder.Status);

            _repositoryMock.Verify(repo =>
                repo.UpdateAsync(orderId, It.Is<Order>(o => o.Status == OrderStatus.Cancelled), It.IsAny<CancellationToken>()),
                Times.Once);

            _loggerMock.Verify(log =>
                log.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, _) => o.ToString()!.Contains("Order Cancelled")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }
    }
}
