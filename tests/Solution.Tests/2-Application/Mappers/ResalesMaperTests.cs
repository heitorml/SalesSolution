using Application.Mapper;
using Domain.Entities;
using Dto.Address;
using Dto.Resales.Requests;
using Orders.Worker.Shared.Mapper;

namespace Solution.Tests._2_Application.Mappers
{
    public class ResalesMaperTests
    {
        [Fact]
        public void ToEntity_Should_Map_ResalesRequestDto_To_Resale()
        {
            // Arrange
            var dto = new ResalesRequestDto
            {
                Name = "Resale Teste",
                Cnpj = "12345678000199",
                ContactName = "João",
                Email = "teste@email.com",
                FantasyName = "Loja X",
                Phone = "11999999999",
                Address = new AddressDto
                {
                    City = "São Paulo",
                    Street = "Rua Teste",
                    ZipCode = "01010101",
                    Number = "123"
                }
            };

            // Act
            var result = ResalesMaper.ToEntity(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Cnpj, result.Cnpj);
            Assert.Equal(dto.ContactName, result.ContactName);
            Assert.Equal(dto.Email, result.Email);
            Assert.Equal(dto.FantasyName, result.FantasyName);
            Assert.Equal(dto.Phone, result.Phone);
            Assert.True(result.Active);
            Assert.NotNull(result.Address);
            Assert.Single(result.Address);
            Assert.Equal(dto.Address.City, result.Address[0].City);
            Assert.True(result.CreateAt <= DateTime.UtcNow);
        }

        [Fact]
        public void ToResponseDto_Should_Map_Resale_To_ResalesResponseDto()
        {
            // Arrange
            var resale = new Resale
            {
                Id = "resale123",
                Name = "Resale Mapeado",
                Cnpj = "98765432000188",
                ContactName = "Maria",
                Email = "maria@email.com",
                FantasyName = "Loja Y",
                Phone = "11988888888",
                Active = true,
                CreateAt = DateTime.UtcNow,
                Address = new List<Address>
            {
                new Address
                {
                    Name = "Rua ABC",
                    City = "Rio de Janeiro",
                    ZipCode = "20202020"
                }
            }
            };

            // Act
            var result = ResalesMaper.ToResponseDto(resale);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(resale.Id, result.Id);
            Assert.Equal(resale.Name, result.Name);
            Assert.Equal(resale.Cnpj, result.Cnpj);
            Assert.Equal(resale.ContactName, result.ContactName);
            Assert.Equal(resale.Email, result.Email);
            Assert.Equal(resale.FantasyName, result.FantasyName);
            Assert.Equal(resale.Phone, result.Phone);
            Assert.Equal(resale.Active, result.Active);
            Assert.Equal(resale.CreateAt, result.CreateAt);
            Assert.NotNull(result.Addresses);
            Assert.Single(result.Addresses);
            Assert.Equal(resale.Address[0].City, result.Addresses[0].City);
        }
    }
}
