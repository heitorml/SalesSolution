namespace Solution.Tests._1_Presentation.Consumers
{
    public class ShippingToSupplierConsumerTests
    {
        //[Fact]
        //public async Task Consume_Should_Call_UseCase_And_Log_Success()
        //{
        //    // Arrange
        //    var orderId = Guid.NewGuid();
        //    var message = new ReadyForShippingOrder { OrderId = orderId };
        //    var contextMock = new Mock<ConsumeContext<ReadyForShippingOrder>>();
        //    contextMock.Setup(c => c.Message).Returns(message);

        //    var loggerMock = new Mock<ILogger<ShippingToSupplierConsumer>>();
        //    var useCaseMock = new Mock<IShippingToSupplierUseCase>();
        //    useCaseMock.Setup(x => x.Execute(orderId.ToString(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success("OK"));

        //    var consumer = new ShippingToSupplierConsumer(useCaseMock.Object, loggerMock.Object);

        //    // Act
        //    await consumer.Consume(contextMock.Object);

        //    // Assert
        //    useCaseMock.Verify(x => x.Execute(orderId.ToString(), It.IsAny<CancellationToken>()), Times.Once);
        //    loggerMock.Verify(
        //        x => x.Log(
        //            LogLevel.Information,
        //            It.IsAny<EventId>(),
        //            It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("ReslesOrders - Finalized")),
        //            null,
        //            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
        //        Times.Once
        //    );
        //}

        //[Fact]
        //public async Task Consume_Should_Log_Error_And_Throw_When_UseCase_Fails()
        //{
        //    // Arrange
        //    var orderId = Guid.NewGuid();
        //    var message = new ReadyForShippingOrder { OrderId = orderId };
        //    var contextMock = new Mock<ConsumeContext<ReadyForShippingOrder>>();
        //    contextMock.Setup(c => c.Message).Returns(message);

        //    var loggerMock = new Mock<ILogger<ShippingToSupplierConsumer>>();
        //    var useCaseMock = new Mock<IShippingToSupplierUseCase>();
        //    var exception = new Exception("Something went wrong");
        //    useCaseMock.Setup(x => x.Execute(orderId.ToString(), It.IsAny<CancellationToken>())).ThrowsAsync(exception);

        //    var consumer = new ShippingToSupplierConsumer(useCaseMock.Object, loggerMock.Object);

        //    // Act & Assert
        //    var ex = await Assert.ThrowsAsync<Exception>(() => consumer.Consume(contextMock.Object));
        //    Assert.Equal("Something went wrong", ex.Message);

        //    loggerMock.Verify(
        //        x => x.Log(
        //            LogLevel.Error,
        //            It.IsAny<EventId>(),
        //            It.Is<It.IsAnyType>((v, t) => true),
        //            exception,
        //            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
        //        Times.Once
        //    );
        //}
    }

    // Mock class
    public class ReadyForShippingOrder
    {
        public Guid OrderId { get; set; }
    }

    // Mock Result wrapper (você deve adaptar isso conforme sua implementação real)
    public class Result
    {
        public object Value { get; set; }
        public static Result Success(object value) => new Result { Value = value };
    }

}
