using Infrastructure.Repoistories;
using Infrastructure.Repoistories.MongoDb;
using Infrastructure.Telemetry;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure
{
    [ExcludeFromCodeCoverage]
    public static class Setup
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            MongoDbMapping.RegisterMappings();
            services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));
            services.AddMonitoringConfiguration(configuration);
        }
    }
}
