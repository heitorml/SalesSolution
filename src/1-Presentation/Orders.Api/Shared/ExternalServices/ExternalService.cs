using Moq;
using Orders.Api.Entities;

namespace Orders.Api.Shared.ExternalServices
{
    public class ExternalService
    {

        public IExternalService CreateExternalServiceMock(bool erroSimulation)
        {
            var mockApi = new Mock<IExternalService>();

            if (erroSimulation)
                mockApi.Setup(api =>
                    api.Send(
                        It.IsAny<Order>(),
                        It.IsAny<CancellationToken>()))
                        .ThrowsAsync(new Exception());
            else
                mockApi.Setup(api =>
                    api.Send(
                        It.IsAny<Order>(),
                        It.IsAny<CancellationToken>()))
                        .Returns(Task.FromResult(true));

            return mockApi.Object;
        }
    }
}
