using Application.Mapper;
using Domain.Entities;
using Dto.OrderItems.Requests;
using Orders.Worker.Shared.Mapper;
using Orders.Worker.Shared.Requests;

namespace Solution.Tests._2_Application.Mappers
{
    public class OrderItemsMaperTests
    {
        [Fact]
        public void ToEntity_FromRequestDtoList_Should_Map_Correctly()
        {
            // Arrange
            var dtoList = new List<OrderItemsRequestDto>
        {
            new OrderItemsRequestDto { Name = "Item A", Description = "Desc A", Price = 10.5m, Quantity = 2 },
            new OrderItemsRequestDto { Name = "Item B", Description = "Desc B", Price = 5.25m, Quantity = 1 }
        };

            // Act
            var result = OrderItemsMaper.ToEntity(dtoList);

            // Assert
            Assert.Equal(dtoList.Count, result.Count);
            Assert.Equal("Item A", result[0].Name);
            Assert.Equal("Desc B", result[1].Description);
            Assert.Equal(10.5m, result[0].Price);
            Assert.Equal(1, result[1].Quantity);
        }

        [Fact]
        public void ToResponseDto_FromOrderItemsList_Should_Map_Correctly()
        {
            // Arrange
            var itemsList = new List<OrderItems>
        {
            new OrderItems { Name = "Item X", Description = "Descrição X", Price = 20, Quantity = 5 },
            new OrderItems { Name = "Item Y", Description = "Descrição Y", Price = 30, Quantity = 10 }
        };

            // Act
            var result = OrderItemsMaper.ToReponseDto(itemsList);

            // Assert
            Assert.Equal(itemsList.Count, result.Count);
            Assert.Equal("Item X", result[0].Name);
            Assert.Equal("Descrição Y", result[1].Description);
            Assert.Equal(20, result[0].Price);
            Assert.Equal(10, result[1].Quantity);
        }
    }
}
