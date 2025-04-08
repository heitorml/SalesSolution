using Resales.Api.Shared.Repoistories;
using Resales.Api.Shared.Repoistories.MongoDb;

namespace Resales.Api.Shared.Configuration
{
    public static class InfraConfiguration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDB"));
            MongoDbMapping.RegisterMappings();
            services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));
            services.AddMonitoringConfiguration(configuration);
        }
    }
}
