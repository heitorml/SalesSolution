using Microsoft.AspNetCore.Http.HttpResults;
using Orders.Api.Shared.Responses;
using System.Diagnostics;
using System.Text.Json;

namespace Orders.Api.Features.OrdersSupplier
{
    public static class CreateOrderSupplierEndpoint
    {
        public static void MapCreateOrdersSupplierEndpoints(RouteGroupBuilder groupEndpoint)
        {

            groupEndpoint.MapPost("/{resaleId}", async Task<Results<Ok<OrderResponse>, BadRequest<object>, StatusCodeHttpResult>> (
                string resaleId,
                ICreateOrderSupplierFeature useCase,
                ActivitySource activitySource,
                CancellationToken cancellationToken) =>
            {
                using var activity = activitySource.StartActivity("ReslesOrders");
                activity?.AddEvent(new ActivityEvent("ReslesOrders - Started"));
                activity?.SetTag("payload.resaleId", resaleId);

                try
                {
                    var result = await useCase.Execute(resaleId, cancellationToken);
                    if (result.IsError)
                    {
                        activity?.AddEvent(new ActivityEvent($"Error: ReslesOrders"));
                        activity?.SetTag("payload.usecaseError", JsonSerializer.Serialize(result.Errors.Distinct()));
                        activity?.SetStatus(ActivityStatusCode.Error, result.FirstError.Description);
                        return TypedResults.StatusCode(400);
                    }

                    activity?.SetTag("payload.response", JsonSerializer.Serialize(result.Value));
                    activity?.AddEvent(new ActivityEvent("ReslesOrders - Finalized"));
                    return TypedResults.Ok(result.Value);
                }
                catch (Exception ex)
                {
                    activity?.AddEvent(new ActivityEvent("Exception"));
                    activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                    return TypedResults.StatusCode(500);
                }
            })
            .WithName("CreateOrderSupplier")
            .Produces<OrderResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
        }
    }
}
