using Microsoft.AspNetCore.Http.HttpResults;
using Resales.Api.Shared.Responses;
using System.Diagnostics;
using System.Text.Json;

namespace Resales.Api.Features.GetById
{
    public static class GetResaleByIdEndpoint
    {
        public static void MapGetResaleByIdEndpoints(RouteGroupBuilder groupEndpoint)
        {
            groupEndpoint.MapGet("/{id}", async Task<Results<Ok<ResalesResponse>, BadRequest<object>, StatusCodeHttpResult>> (
                 string id,
                 IGetResaleByIdFeature feature,
                 ActivitySource activitySource,
                 CancellationToken cancellationToken) =>
            {
                using var activity = activitySource.StartActivity("GetByIdResale");
                activity?.AddEvent(new ActivityEvent("GetByIdResale - Started"));
                activity?.SetTag("payload.request", id);

                try
                {

                    var result = await feature.Execute(id, cancellationToken);
                    if (result.IsError)
                        return TypedResults.StatusCode(404);

                    activity?.SetTag("payload.response", JsonSerializer.Serialize(result.Value));
                    activity?.AddEvent(new ActivityEvent("GetByIdResale - Finalized"));
                    return TypedResults.Ok(result.Value);
                }
                catch (Exception ex)
                {
                    activity?.AddEvent(new ActivityEvent("Exception"));
                    activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                    return TypedResults.StatusCode(500);
                }
            })
              .WithName("GetByIdResale")
              .Produces<OrderResponse>(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status400BadRequest)
              .Produces(StatusCodes.Status500InternalServerError);
        }
    }
}
