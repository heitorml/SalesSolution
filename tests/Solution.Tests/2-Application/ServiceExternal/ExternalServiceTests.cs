using Application.ExternalServices;
using Domain.Entities;

namespace Solution.Tests._2_Application.ServiceExternal
{
    public class ExternalServiceTests
    {
        [Fact]
        public async Task CreateExternalServiceMock_Should_Return_Success_When_ErroSimulation_IsFalse()
        {
            // Arrange
            var externalService = new ExternalService();
            var serviceMock = externalService.CreateExternalServiceMock(false);
            var order = new Order(); 

            // Act
            var exception = await Record.ExceptionAsync(() => serviceMock.Send(order, CancellationToken.None));

            // Assert
            Assert.Null(exception); 
        }

        [Fact]
        public async Task CreateExternalServiceMock_Should_ThrowException_When_ErroSimulation_IsTrue()
        {
            // Arrange
            var externalService = new ExternalService();
            var serviceMock = externalService.CreateExternalServiceMock(true);
            var order = new Order();

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => serviceMock.Send(order, CancellationToken.None));
        }
    }
}
