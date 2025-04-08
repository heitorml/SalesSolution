using Application.ExternalServices;
using Application.UseCases.Orders.ShippingToSupplier;
using CrossCutting.Enums;
using CrossCutting.Errors;
using Domain.Entities;
using Domain.Events.Orders;
using FluentAssertions;
using Infrastructure.Repoistories;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Moq;
using Orders.Worker.Events;

namespace Solution.Tests._2_Application.UseCases.Orders
{
    public class ShippingToSupplierUseCaseTests
    {
        private readonly Mock<IRepository<Order>> _repositoryMock = new();
        private readonly Mock<IExternalService> _externalServiceMock = new();
        private readonly Mock<IBus> _busMock = new();
        private readonly ShippingToSupplierUseCase _useCase;

        public ShippingToSupplierUseCaseTests()
        {
            _useCase = new ShippingToSupplierUseCase(
                _repositoryMock.Object,
                _externalServiceMock.Object,
                _busMock.Object,
                Mock.Of<IConfiguration>() // se não é usado, podemos mockar com vazio
            );
        }

        [Fact]
        public async Task Execute_ShouldReturnError_WhenOrderIsNull()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetByIdAsync("123", It.IsAny<CancellationToken>()))
                           .ReturnsAsync((Order)null);

            // Act
            var result = await _useCase.Execute("123", CancellationToken.None);

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Code.Should().Be(ErrorCatalog.OrderNotFound.Code);
        }

        [Fact]
        public async Task Execute_ShouldReturnError_WhenOrderIsNotReadyForShipping()
        {
            // Arrange
            var order = new Order { Status = OrderStatus.Merged };
            _repositoryMock.Setup(r => r.GetByIdAsync("123", It.IsAny<CancellationToken>()))
                           .ReturnsAsync(order);

            // Act
            var result = await _useCase.Execute("123", CancellationToken.None);

            // Assert
            result.IsError.Should().BeTrue();
            result.FirstError.Code.Should().Be(ErrorCatalog.OrderNotFound.Code);
        }

        [Fact]
        public async Task Execute_ShouldProcessOrder_WhenOrderIsValid()
        {
            // Arrange
            var order = new Order
            {
                Id = "123",
                Status = OrderStatus.ReadyForShipping,
                Items = new List<OrderItems> { new OrderItems { Name = "Produto", Quantity = 10, Price = 100 } },
                Resale = new Resale { Id = "resale-01", Name = "Revenda" },
                Price = 1000
            };

            _repositoryMock.Setup(r => r.GetByIdAsync("123", It.IsAny<CancellationToken>()))
                           .ReturnsAsync(order);

            _externalServiceMock.Setup(s => s.Send(order, It.IsAny<CancellationToken>()))
                                .Returns(Task.FromResult(true));

            _repositoryMock.Setup(r => r.UpdateAsync(order.Id, order, It.IsAny<CancellationToken>()))
                           .Returns(Task.CompletedTask);

            _busMock.Setup(b => b.Publish(It.IsAny<OrderSentToSupplier>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.CompletedTask);

            // Act
            var result = await _useCase.Execute("123", CancellationToken.None);

            // Assert
            result.IsError.Should().BeFalse();
            result.Value.Id.Should().Be(order.Id);

            _externalServiceMock.Verify(s => s.Send(order, It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(r => r.UpdateAsync(order.Id, order, It.IsAny<CancellationToken>()), Times.Once);
            _busMock.Verify(b => b.Publish(It.IsAny<OrderSentToSupplier>(), It.IsAny<CancellationToken>()), Times.Once);

            order.Status.Should().Be(OrderStatus.ShippedToSupplier);
        }
    }
}
