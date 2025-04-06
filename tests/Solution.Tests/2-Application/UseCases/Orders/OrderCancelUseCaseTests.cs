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
        public async Task Execute_Should_CancelOrder_When_ValidInput()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;
            var orderId = "order-123";
            var order = new Order { Id = orderId, Status = OrderStatus.Cancelled };

            var eventDto = new CancelledOrderRequested
            {
                OrderId = orderId
            };

            _repositoryMock
                .Setup(r => r.GetByIdAsync(orderId, cancellationToken))
                .ReturnsAsync(order);

            _repositoryMock
                .Setup(r => r.UpdateAsync(orderId, It.IsAny<Order>(), cancellationToken))
                .Returns(Task.CompletedTask);

            // Act
            await _useCase.Execute(eventDto, cancellationToken);

            // Assert
            Assert.Equal(OrderStatus.Cancelled, order.Status);

            _repositoryMock.Verify(r => r.GetByIdAsync(orderId, cancellationToken), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(orderId, order, cancellationToken), Times.Once);

            _loggerMock.Verify(
                log => log.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Order Cancelled")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }
    }
}
