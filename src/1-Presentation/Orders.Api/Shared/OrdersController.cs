using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Orders.Api.Shared
{
    [ExcludeFromCodeCoverage]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("orders/")]
    [Produces("application/json")]
    public partial class OrdersController : ControllerBase
    {
        private readonly ActivitySource _activitySource;

        public OrdersController(ActivitySource activitySource)
        {
            _activitySource = activitySource;
        }

    }
}
