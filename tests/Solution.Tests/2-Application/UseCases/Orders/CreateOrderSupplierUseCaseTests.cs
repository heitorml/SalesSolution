using Application.UseCases.Orders.OrdersSupplier;
using CrossCutting.Errors;
using Domain.Entities;
using Domain.Events.Orders;
using Infrastructure.Repoistories;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using Solution.Tests.Init;
using System.Linq.Expressions;

namespace Solution.Tests._2_Application.UseCases.Orders
{

    public class CreateOrderSupplierUseCaseTests
    {
        private readonly Mock<ILogger<CreateOrderSupplierUseCase>> _loggerMock = new();
        private readonly Mock<IRepository<Order>> _repositoryMock = new();
        private readonly Mock<IBus> _busMock = new();
        private readonly CreateOrderSupplierUseCase _useCase;

        public CreateOrderSupplierUseCaseTests()
        {
            _useCase = new CreateOrderSupplierUseCase(
                _loggerMock.Object,
                _repositoryMock.Object,
                _busMock.Object);
        }

        [Fact]
        public async Task Should_ReturnError_When_NoOrdersFound()
        {
            _repositoryMock.Setup(r =>
                r.FindAsync(It.IsAny<Expression<Func<Order, bool>>>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Order>());

            var result = await _useCase.Execute("resale-123", CancellationToken.None);

            Assert.True(result.IsError);
            Assert.Equal(ErrorCatalog.OrderNotFound.Code, result.FirstError.Code);
        }

        [Fact]
        public async Task Should_ReturnError_When_TotalQuantityLessThan1000()
        {
            List<Order> orders = OrdersInit.CreateOrderQuantityLessThan1000();

            _repositoryMock.Setup(r => r.FindAsync(It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(orders);

            var result = await _useCase.Execute("resale-123", CancellationToken.None);

            Assert.True(result.IsError);
            Assert.Equal(ErrorCatalog.MinimumQuantityNotReached.Code, result.FirstError.Code);
        }

        [Fact]
        public async Task Should_CreateOrder_And_UpdateMergedOrders_When_Valid()
        {
            List<Order> orders = OrdersInit.CreateOrder();

            _repositoryMock.Setup(r => r.FindAsync(It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<CancellationToken>()))
                           .ReturnsAsync(orders);

            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()))
                           .Returns(Task.CompletedTask);

            _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<string>(), It.IsAny<Order>(), It.IsAny<CancellationToken>()))
                           .Returns(Task.CompletedTask);

            _busMock.Setup(b => b.Publish(It.IsAny<ReadyForShippingOrder>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.CompletedTask);

            var result = await _useCase.Execute("resale-123", CancellationToken.None);

            Assert.False(result.IsError);
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync("order-1", It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Once);
            _busMock.Verify(b => b.Publish(It.IsAny<ReadyForShippingOrder>(), It.IsAny<CancellationToken>()), Times.Once);
        }


    }
}
