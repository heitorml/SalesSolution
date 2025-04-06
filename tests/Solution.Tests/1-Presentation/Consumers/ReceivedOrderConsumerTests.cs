using CrossCutting.Enums;
using Domain.Entities;
using Domain.Events.Orders;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using Orders.Worker.Consumers;

namespace Solution.Tests._1_Presentation.Consumers
{
    public class ReceivedOrderConsumerTests
    {
        [Fact]
        public async Task Consume_Should_LogInformation_When_Message_Is_Consumed()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ReceivedOrderConsumer>>();
            var consumer = new ReceivedOrderConsumer(loggerMock.Object);

            var order = new Order
            {
                Id = "order-1",
                Resale = new Resale { Id = "resale-123" },
                Status = OrderStatus.Received,
                Items = new List<OrderItems>
                    {
                        new() { Name = "Item A", Quantity = 600, Price = 10 },
                        new() { Name = "Item A", Quantity = 500, Price = 10 }
                    }
            };

            var message = new ReceivedOrder
            {
                Id = order.Id,
                Resale = order.Resale,
                Items = order.Items,
                Price = order.Price
            }; 
            var contextMock = new Mock<ConsumeContext<ReceivedOrder>>();
            contextMock.SetupGet(x => x.Message).Returns(message);

            // Act
            await consumer.Consume(contextMock.Object);

            // Assert
            loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Enviar e-mail de pedido criado com sucesso")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }
    }
}
