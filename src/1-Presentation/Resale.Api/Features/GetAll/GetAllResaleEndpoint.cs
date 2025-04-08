using Microsoft.AspNetCore.Http.HttpResults;
using Resales.Api.Shared.Responses;
using System.Diagnostics;

namespace Resales.Api.Features.GetAll
{
    public static class GetAllResaleEndpoint
    {
        public static void MapGetAllResaleEndpoints(RouteGroupBuilder groupEndpoint)
        {
            groupEndpoint.MapGet("/", async Task<Results<Ok<List<ResalesResponse>>, BadRequest<object>, StatusCodeHttpResult>> 
                (
                    IGetAllResaleFeature getAllUseCase,
                    ActivitySource activitySource,
                    CancellationToken cancellationToken) =>
            {
                using var activity = activitySource.StartActivity("GetAllResale");
                activity?.AddEvent(new ActivityEvent("GetAllResale - Started"));

                try
                {
                    var result = await getAllUseCase.Execute(cancellationToken);
                    if (result.IsError)
                        return TypedResults.StatusCode(404);

                    activity?.AddEvent(new ActivityEvent("GetAllResale - Finalized"));
                    return TypedResults.Ok(result.Value);
                }
                catch (Exception ex)
                {

                    activity?.AddEvent(new ActivityEvent("Exception"));
                    activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                    return TypedResults.StatusCode(500);
                }
            })
              .WithName("GetAllResale")
              .Produces<OrderResponse>(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status400BadRequest)
              .Produces(StatusCodes.Status500InternalServerError);
        }
    }
}
