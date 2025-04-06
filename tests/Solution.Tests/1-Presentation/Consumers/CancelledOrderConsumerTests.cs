using Application.UseCases.Orders.OrderCancel;
using Domain.Events.Orders;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using Orders.Worker.Consumers;

namespace Solution.Tests._1_Presentation.Consumers
{
    public class CancelledOrderConsumerTests
    {
        private readonly Mock<ILogger<CancelledOrderConsumer>> _loggerMock;
        private readonly Mock<IOrderCancelUseCase> _cancelUseCaseMock;
        private readonly CancelledOrderConsumer _consumer;

        public CancelledOrderConsumerTests()
        {
            _loggerMock = new Mock<ILogger<CancelledOrderConsumer>>();
            _cancelUseCaseMock = new Mock<IOrderCancelUseCase>();

            _consumer = new CancelledOrderConsumer(_loggerMock.Object, _cancelUseCaseMock.Object);
        }

        [Fact]
        public async Task Consume_Should_Execute_CancelOrderUseCase()
        {
            // Arrange
            var orderId = "order-xyz";
            var message = new CancelledOrderRequested { OrderId = orderId };

            var contextMock = new Mock<ConsumeContext<CancelledOrderRequested>>();
            contextMock.Setup(x => x.Message).Returns(message);

            // Act
            await _consumer.Consume(contextMock.Object);

            // Assert
            _cancelUseCaseMock.Verify(u =>
                u.Execute(It.Is<CancelledOrderRequested>(c => c.OrderId == orderId),
                          CancellationToken.None),
                Times.Once);
        }
    }

}
