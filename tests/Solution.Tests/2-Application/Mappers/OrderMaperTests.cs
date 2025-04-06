using Application.Mapper;
using CrossCutting.Enums;
using Domain.Entities;
using Dto.Address;
using Dto.OrderItems.Requests;
using Dto.Orders.Requests;
using Dto.Resales.Requests;

namespace Solution.Tests._2_Application.Mappers
{
    public class OrderMaperTests
    {
        [Fact]
        public void ToEntity_Should_Map_OrderRequestDto_To_Order()
        {
            // Arrange
            var dto = new OrderRequestDto
            {
                Resale = new ResalesRequestDto
                {
                    Name = "Resale A",
                    Cnpj = "12345678000199",
                    FantasyName = "Loja X",
                    Email = "email@test.com",
                    Phone = "99999999",
                    ContactName = "Contato",
                    Address = new AddressDto { City = "City", Street = "Street", ZipCode = "00000000", Number = "123" }
                },
                Items = new List<OrderItemsRequestDto>
            {
                new OrderItemsRequestDto { Name = "Item 1", Description = "Desc", Price = 10, Quantity = 2 }
            }
            };

            // Act
            var result = OrderMaper.ToEntity(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Resale.Name, result.Resale.Name);
            Assert.Single(result.Items);
            Assert.True(result.CreatAt <= DateTime.Now);
        }

        [Fact]
        public void ToEntityByStatus_Should_Set_Status_And_UpdatedAt()
        {
            // Arrange
            var dto = new OrderRequestDto
            {
                Resale = new ResalesRequestDto
                {
                    Name = "Resale B",
                    Cnpj = "98765432000188",
                    FantasyName = "Loja Y",
                    Email = "teste@teste.com",
                    Phone = "88888888",
                    ContactName = "Contato 2",
                    Address = new AddressDto { City = "City", Street = "Street", ZipCode = "11111111", Number = "456" }
                },
                Items = new List<OrderItemsRequestDto>
            {
                new OrderItemsRequestDto { Name = "Item 2", Description = "Desc2", Price = 20, Quantity = 1 }
            }
            };

            var expectedStatus = OrderStatus.Received;

            // Act
            var result = OrderMaper.ToEntityByStatus(dto, expectedStatus);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedStatus, result.Status);
            Assert.Equal(dto.Items.Count, result.Items.Count);
            Assert.True(result.UpdatedAt <= DateTime.Now);
        }

        [Fact]
        public void ToResponseDto_Should_Map_Order_To_ResponseDto()
        {
            // Arrange
            var order = new Order()
            {
                Id = "order123",
                Items = new List<OrderItems>
            {
                new OrderItems { Name = "Item Test", Description = "Desc", Quantity = 1, Price = 50 }
            },
                Price = 50
            };

            // Act
            var result = OrderMaper.ToResponseDto(order);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(order.Id, result.Id);
            Assert.Equal(order.Price, result.Price);
            Assert.Single(result.Items);
            Assert.Equal(order.Items.First().Name, result.Items.First().Name);
        }
    }
}
