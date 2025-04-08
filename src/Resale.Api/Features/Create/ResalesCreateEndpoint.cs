using Microsoft.AspNetCore.Http.HttpResults;
using Resales.Api.Shared.Requests;
using Resales.Api.Shared.Responses;
using Resales.Api.Shared.Validator;
using System.Diagnostics;

namespace Resales.Api.Features.Create
{
    public static class ResalesCreateEndpoint
    {
        public static void MapCreateResalesEndpoints(RouteGroupBuilder groupEndpoint)
        {

            groupEndpoint.MapPost("/", async Task<Results<Ok<string>, BadRequest<object>, StatusCodeHttpResult>> (
                ResalesCreateRequest request,
                IResalesCreateFeature feature,
                ActivitySource activitySource,
                CancellationToken cancellationToken) =>
            {
                using var activity = activitySource.StartActivity("CreateResale");
                activity?.AddEvent(new ActivityEvent("Create Resale - Started"));
                activity?.SetTag("payload.request", request);

                try
                {
                    var validator = new ResalesRequestValidator();
                    var validationResult = validator.Validate(request);

                    if (!validationResult.IsValid)
                    {
                        activity?.AddEvent(new ActivityEvent("Request Invalided"));
                        activity?.SetTag("payload.validationError", validationResult.Errors.Distinct());
                        return TypedResults.StatusCode(400);
                    }

                    var result = await feature.Execute(request, cancellationToken);
                    if (result.IsError)
                    {
                        activity?.AddEvent(new ActivityEvent($"Error: CreateUseCase"));
                        activity?.SetTag("payload.usecaseError", result.Errors.Distinct());
                        return TypedResults.StatusCode(400);
                    }

                    activity?.SetTag("payload.response", result.Value);
                    activity?.AddEvent(new ActivityEvent("Create Resale - Finalized"));
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
