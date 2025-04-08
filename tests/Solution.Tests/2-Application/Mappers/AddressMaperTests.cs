using Application.Mapper;
using Domain.Entities;
using Dto.Address;
using Orders.Worker.Shared.Mapper;
using Orders.Worker.Shared.Requests;

namespace Solution.Tests._2_Application.Mappers
{
    public class AddressMaperTests
    {
        [Fact]
        public void ToEntity_FromDtoList_Should_Map_Correctly()
        {
            // Arrange
            var dtoList = new List<AddressDto>
        {
            new AddressDto { Street = "Rua A", ZipCode = "01000-000", City = "São Paulo" },
            new AddressDto { Street = "Rua B", ZipCode = "02000-000", City = "Rio de Janeiro" }
        };

            // Act
            var result = AddressMaper.ToEntity(dtoList);

            // Assert
            Assert.Equal(dtoList.Count, result.Count);
            Assert.Equal("Rua A", result[0].Name);
            Assert.Equal("02000-000", result[1].ZipCode);
        }

        [Fact]
        public void ToDto_FromEntityList_Should_Map_Correctly()
        {
            // Arrange
            var entityList = new List<Address>
        {
            new Address { Name = "Av. Paulista", ZipCode = "01310-100", City = "São Paulo" },
            new Address { Name = "Av. Brasil", ZipCode = "22290-140", City = "Rio de Janeiro" }
        };

            // Act
            var result = AddressMaper.ToDto(entityList);

            // Assert
            Assert.Equal(entityList.Count, result.Count);
            Assert.Equal("Av. Paulista", result[0].Street);
            Assert.Equal("Rio de Janeiro", result[1].City);
        }

        [Fact]
        public void ToEntity_FromSingleDto_Should_Return_ListWithOneItem()
        {
            // Arrange
            var dto = new AddressDto
            {
                Street = "Rua das Flores",
                ZipCode = "04567-890",
                City = "Curitiba"
            };

            // Act
            var result = AddressMaper.ToEntity(dto);

            // Assert
            Assert.Single(result);
            Assert.Equal("Rua das Flores", result.First().Name);
            Assert.Equal("Curitiba", result.First().City);
        }
    }
}
