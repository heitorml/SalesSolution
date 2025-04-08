using Microsoft.AspNetCore.Http.HttpResults;
using Orders.Api.Shared.Responses;
using System.Diagnostics;
using System.Text.Json;

namespace Orders.Api.Features.OrdersResale
{
    public static class CreateOrdersResalesEndpoint
    {
        public static void MapCreateOrdersResaleEndpoints(RouteGroupBuilder groupEndpoint)
        {

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
            /// <param name="useCase">Caso de uso para criação de pedido para a revenda.</param>
            /// <param name="cancellationToken">Cancellation token.</param>
            /// <returns>Codigo do pedido e lista de itens</returns> 
            /// <response code="201">Returns OrderResponseDto</response>
            groupEndpoint.MapPost("/", async Task<Results<Ok<OrderResponse>, BadRequest<object>, StatusCodeHttpResult>> (
                CreateOrderResalesResquest orderDto,
                ICreateOrderResalesFeature useCase,
                ActivitySource activitySource,
                CancellationToken cancellationToken) =>
            {
                using var activity = activitySource.StartActivity("CreateOrder");
                activity?.AddEvent(new ActivityEvent("Create Order - Started"));
                activity?.SetTag("payload.request", JsonSerializer.Serialize(orderDto));

                var validator = new CreateOrderResalesRequestValidator();
                var validationResult = validator.Validate(orderDto);

                if (!validationResult.IsValid)
                {
                    activity?.AddEvent(new ActivityEvent("Request Invalid"));
                    activity?.SetTag("payload.validationError", JsonSerializer.Serialize(validationResult.Errors));
                    activity?.SetStatus(ActivityStatusCode.Error, "Validation Fail");
                    return TypedResults.StatusCode(400);
                }

                try
                {
                    var result = await useCase.Execute(orderDto, cancellationToken);
                    if (result.IsError)
                    {
                        activity?.AddEvent(new ActivityEvent("Error: CreateOrder"));
                        activity?.SetTag("payload.usecaseError", JsonSerializer.Serialize(result.Errors));
                        activity?.SetStatus(ActivityStatusCode.Error, result.FirstError.Description);
                        return TypedResults.StatusCode(400);
                    }

                    activity?.SetTag("payload.response", JsonSerializer.Serialize(result.Value));
                    activity?.AddEvent(new ActivityEvent("Create Order - Finalized"));
                    return TypedResults.Ok(result.Value);
                }
                catch (Exception ex)
                {
                    activity?.AddEvent(new ActivityEvent("Exception"));
                    activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                    return TypedResults.StatusCode(500);
                }
            })
            .WithName("CreateOrderResale")
            .WithSummary("Criação de um pedido.")
            .WithDescription("Cria um novo pedido de revenda com base nos dados fornecidos.")
            .Produces<OrderResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
        }
    }
}
