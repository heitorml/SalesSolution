using Microsoft.AspNetCore.Http.HttpResults;
using Resales.Api.Shared.Requests;
using Resales.Api.Shared.Responses;
using Resales.Api.Shared.Validator;
using System.Diagnostics;
using System.Text.Json;

namespace Resales.Api.Features.Updade
{
    public static class UpdateResaleEndpoint
    {
        public static void MapUpdateResalesEndpoints(RouteGroupBuilder groupEndpoint)
        {

            groupEndpoint.MapPatch("/", async Task<Results<Ok<ResalesResponse>, BadRequest<object>, StatusCodeHttpResult>> (
                string id,
                ResaleUpdateRequest request,
                IUpdateResaleFeature feature,
                ActivitySource activitySource,
                CancellationToken cancellationToken) =>
            {
                using var activity = activitySource.StartActivity("UpdateResale");
                activity?.AddEvent(new ActivityEvent("UpdateResale - Started"));
                activity?.SetTag("payload.request", request);
                activity?.SetTag("payload.id", id);
                activity?.SetTag("payload.request", JsonSerializer.Serialize(request));

                try
                {
                    var validator = new ResalesUpdatRequestValidator();
                    var validationResult = validator.Validate(request);

                    if (!validationResult.IsValid)
                    {
                        activity?.AddEvent(new ActivityEvent("Request Invalided"));
                        activity?.SetTag("payload.validationError", JsonSerializer.Serialize(validationResult.Errors.Distinct()));
                        return TypedResults.StatusCode(400);
                    }

                    var result = await feature.Execute(id, request, cancellationToken);
                    if (result.IsError)
                    {
                        activity?.AddEvent(new ActivityEvent("Error: UpdateUseCase"));
                        activity?.SetTag("payload.usecaseError", JsonSerializer.Serialize(result.Errors.Distinct()));
                        return TypedResults.StatusCode(400);
                    }

                    activity?.SetTag("payload.response", JsonSerializer.Serialize(result.Value));
                    activity?.AddEvent(new ActivityEvent("UpdateResale - Finalized"));
                    return TypedResults.Ok(result.Value);
                }
                catch (Exception ex)
                {
                    activity?.AddEvent(new ActivityEvent("Exception"));
                    activity?.SetStatus(ActivityStatusCode.Error, ex.Message);
                    return TypedResults.StatusCode(500);
                }
            })
         .WithName("UpdateResale")
         .Produces<OrderResponse>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status400BadRequest)
         .Produces(StatusCodes.Status500InternalServerError);
        }
    }
}
