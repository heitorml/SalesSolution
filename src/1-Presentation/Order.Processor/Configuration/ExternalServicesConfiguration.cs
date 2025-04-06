using Application.ExternalServices;
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
            var erroTestExternalApi = configuration.GetValue<bool>("SimularErroApiExterna");
            var mockApi = new ExternalService()
                .CreateExternalServiceMock(erroTestExternalApi);

            services.AddSingleton(mockApi);

            services.AddRefitClient<IExternalService>()
                    .ConfigureHttpClient(c =>
                    {
                        c.BaseAddress = new Uri(configuration["ExternalSericesUrls:Mock"]);
                    })
                    .AddPolicyHandler(GetRetryPolicy(configuration))
                    .AddPolicyHandler(GetCircuitBreakerPolicy(configuration))
                    .AddPolicyHandler(GetTimeoutPolicy(configuration));
        }
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(IConfiguration configuration)
        {
            Random jitter = new();
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: retryAttempt =>
                        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)) +
                        TimeSpan.FromMilliseconds(jitter.Next(0, 100)),
                    onRetry: (response, timespan, retryCount, context) =>
                    {
                        var activity = Activity.Current;
                        activity?.AddEvent(new ActivityEvent($"Retry {retryCount} after {timespan.TotalSeconds}s"));
                        Console.WriteLine($"Tentativa {retryCount} - Esperando {timespan.TotalSeconds} segundos");
                    });
        }

        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(IConfiguration configuration)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 5,
                    durationOfBreak: TimeSpan.FromSeconds(30),
                    onBreak: (outcome, breakDelay) =>
                    {
                        Activity.Current?.AddEvent(new ActivityEvent($"🔌 Circuit open: {breakDelay.TotalSeconds}s"));
                        Console.WriteLine($"Circuito aberto por {breakDelay.TotalSeconds} segundos devido a: {outcome.Exception?.Message}");
                    },
                    onReset: () =>
                    {
                        Console.WriteLine("Circuito fechado. Tentativas retomadas.");
                        Activity.Current?.AddEvent(new ActivityEvent("Circuit reset"));
                    },
                    onHalfOpen: () => Console.WriteLine("Circuito em estado intermediário (Half-Open)."));
        }

        public static IAsyncPolicy<HttpResponseMessage> GetTimeoutPolicy(IConfiguration configuration)
        {
            return Policy.TimeoutAsync<HttpResponseMessage>(10); // 10 segundos
        }
    }
}
