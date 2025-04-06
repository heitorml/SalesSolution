using Application.ExternalServices;
using MassTransit;
using Polly;
using Polly.Extensions.Http;
using Refit;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Orders.Worker.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class ExternalServicesConfiguration
    {

        public static void AddExternalServices(this IServiceCollection services, IConfiguration configuration)
        {
            var erroTestExternalApi = configuration.GetValue<bool>("SimulateErrorExternalService");
            var mockApi = new ExternalService()
                .CreateExternalServiceMock(erroTestExternalApi);

            services.AddSingleton(mockApi);

            if (erroTestExternalApi)
            {
                services.AddRefitClient<IExternalService>()
                    .ConfigureHttpClient(c =>
                    {
                        c.BaseAddress = new Uri(configuration["ExternalSericesUrls:Mock"]);
                    })
                    .AddPolicyHandler((sp, msg) => GetRetryPolicy(configuration, sp))
                    .AddPolicyHandler((sp, msg) => GetCircuitBreakerPolicy(configuration, sp))
                    .AddPolicyHandler(GetTimeoutPolicy(configuration));
            }
        }

        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(IConfiguration configuration, IServiceProvider sp)
        {
            var optionsResilience = configuration.GetSection("ResiliencePolicy").Get<ResiliencePolicyOptions>();
            var publishEndpoint = sp.GetRequiredService<IPublishEndpoint>();

            Random jitter = new();
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(
                    retryCount: optionsResilience.Retry,
                    sleepDurationProvider: retryAttempt =>
                        TimeSpan.FromSeconds(Math.Pow(optionsResilience.RetrySecondInitial, retryAttempt)) +
                        TimeSpan.FromMilliseconds(jitter.Next(0, 100)),
                    onRetry: (response, timespan, retryCount, context) =>
                    {
                        var activity = Activity.Current;
                        activity?.AddEvent(new ActivityEvent($"Retry {retryCount} after {timespan.TotalSeconds}s"));
                        Console.WriteLine($"Number try {retryCount} - await {timespan.TotalSeconds} seconds");
                    });
        }

        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(IConfiguration configuration, IServiceProvider sp)
        {
            var optionsResilience = configuration.GetSection("ResiliencePolicy").Get<ResiliencePolicyOptions>();
            var publishEndpoint = sp.GetRequiredService<IPublishEndpoint>();

            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: optionsResilience.DisarmCircuitAfterErros,
                    durationOfBreak: TimeSpan.FromSeconds(optionsResilience.DisarmCircuitTimmer),
                    onBreak: (outcome, breakDelay) =>
                    {
                        var reason = outcome.Exception?.Message ?? outcome.Result?.ReasonPhrase ?? "Erro inexpected";
                        Console.WriteLine($"Circuit breaker open {breakDelay.TotalSeconds} seconds. Reason: {reason}");
                        Activity.Current?.AddEvent(new ActivityEvent("Circuit breaker open"));
                    },
                    onReset: () =>
                    {
                        Activity.Current?.AddEvent(new ActivityEvent("Circuit breaker resetado"));
                    },
                    onHalfOpen: () =>
                    {
                        Activity.Current?.AddEvent(new ActivityEvent("Circuit breaker Half-Open"));
                    });
        }

        public static IAsyncPolicy<HttpResponseMessage> GetTimeoutPolicy(IConfiguration configuration)
        {
            var optionsResilience = configuration.GetSection("ResiliencePolicy").Get<ResiliencePolicyOptions>();
            return Policy.TimeoutAsync<HttpResponseMessage>(optionsResilience.TimeoutPolicy); // 10 segundos
        }
    }
}
