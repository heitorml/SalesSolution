using Application.UseCases.Orders.OrdersSupplier;
using Application.UseCases.Orders.Receive;
using Application.Validators;
using Dto.Orders.Reponses;
using Dto.Orders.Requests;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Orders.Api.Controllers
{
    [ExcludeFromCodeCoverage]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("orders/")]
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        private readonly ActivitySource _activitySource;

        public OrdersController(ActivitySource activitySource)
        {
            _activitySource = activitySource;
        }

        /// <summary>
        /// Criação de um pedido.
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     POST /{
        ///             "resaleId": "67f19f1c10089332038b11e0",
        ///             "resale": {
        ///                  "name": "Wimpsys Distribuidora de Bebidas",
        ///                  "fantasyName": "Wimpsys Bebidas",
        ///                  "phone": "27996218842",
        ///                  "contactName": "Heitor Machado",
        ///                  "email": "machado.loureiro@gmail.com",
        ///                  "cnpj": "45855653000190",
        ///                  "address": 
        ///                  {
        ///                    "street": "Av. 7",
        ///                    "number": "s/n",
        ///                    "zipCode": "29100200",
        ///                    "city": "Vila Velha"
        ///                  }
        ///             },
        ///             "items": [
        ///                  {
        ///                      "name": "Chandon",
        ///                    "description": "Chandon",
        ///                    "quantity": 10,
        ///                    "price": 129.95
        ///                  },
        ///                  {
        ///                      "name": "Ciroc",
        ///                    "description": "Vodka",
        ///                    "quantity": 10,
        ///                    "price": 98
        ///                  },
        ///                  {
        ///                      "name": "Michelob Ultra",
        ///                    "description": "Cervja sem glutén",
        ///                    "quantity": 500,
        ///                    "price": 5
        ///                  },
        ///                  {
        ///                      "name": "Stela Artois Ultra",
        ///                    "description": "Cerveja stela artois ultra",
        ///                    "quantity": 500,
        ///                    "price": 5.10
        ///                  }
        ///             ]
        ///         }
        /// </remarks>
        /// <param name="orderDto">Dto de requisição de pedido.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Codigo do pedido e lista de itens</returns> 
        /// <response code="201">Returns OrderResponseDto</response>
        [HttpPost]
        [ProducesResponseType(typeof(OrderResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrderResponseDto>> CreateOrder(
            [FromBody] OrderRequestDto orderDto,
            [FromServices] ICreateOrderResalesUseCase useCase,
            CancellationToken cancellationToken)
        {
            using var activity = _activitySource.StartActivity("CreateOrder");
            activity?.AddEvent(new ActivityEvent("Create Order - Started"));
            activity?.SetTag("payload.request", JsonSerializer.Serialize(orderDto));

            try
            {
                var validator = new OrderRequestDtoValidator();
                var validationResult = validator.Validate(orderDto);

                if (!validationResult.IsValid)
                {
                    activity?.AddEvent(new ActivityEvent("Request Invalided"));
                    activity?.SetTag("payload.validationError", JsonSerializer.Serialize(validationResult.Errors.Distinct()));
                    activity?.SetStatus(ActivityStatusCode.Error, "Validation Fail");
                    return BadRequest(validationResult.Errors);
                }

                var result = await useCase.Execute(orderDto, cancellationToken);
                if (result.IsError)
                {
                    activity?.AddEvent(new ActivityEvent($"Error: CreateOrder"));
                    activity?.SetTag("payload.usecaseError", JsonSerializer.Serialize(result.Errors.Distinct()));
                    activity?.SetStatus(ActivityStatusCode.Error, result.FirstError.Description);
                    return BadRequest(ErrorOr<OrderResponseDto>.From(result.Errors));
                }

                activity?.SetTag("payload.response", JsonSerializer.Serialize(result.Value));
                activity?.AddEvent(new ActivityEvent("Create Order - Finalized"));
                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                activity?.AddEvent(new ActivityEvent("Exception"));
                activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                return StatusCode(500, "Erro inesperado.");
            }
        }




        /// <summary>
        /// Criação de um pedido a ser enviado para o Fornecedor.
        /// </summary>
        /// <param name="resaleId">Identificador da revenda.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Codigo do pedido e lista de itens</returns> 
        /// <response code="201">Returns OrderResponseDto</response>
        [ProducesResponseType(typeof(OrderResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("{resaleId}")]
        public async Task<ActionResult<OrderResponseDto>> JoinOrders(
            string resaleId,
            [FromServices] ICreateOrderSupplierUseCase joinOrdersUseCase,
            CancellationToken cancellationToken)
        {
            using var activity = _activitySource.StartActivity("ReslesOrders");
            activity?.AddEvent(new ActivityEvent("ReslesOrders - Started"));
            activity?.SetTag("payload.resaleId", resaleId);

            try
            {
                var result = await joinOrdersUseCase.Execute(resaleId, cancellationToken);
                if (result.IsError)
                {
                    activity?.AddEvent(new ActivityEvent($"Error: ReslesOrders"));
                    activity?.SetTag("payload.usecaseError", JsonSerializer.Serialize(result.Errors.Distinct()));
                    activity?.SetStatus(ActivityStatusCode.Error, result.FirstError.Description);
                    return BadRequest(result.Errors);
                }

                activity?.SetTag("payload.response", JsonSerializer.Serialize(result.Value));
                activity?.AddEvent(new ActivityEvent("ReslesOrders - Finalized"));
                return Ok(result.Value);
            }
            catch (Exception ex)
            {
                activity?.AddEvent(new ActivityEvent("Exception"));
                activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                return StatusCode(500, "Erro inesperado.");
            }
        }
    }
}
