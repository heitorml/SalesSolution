using Orders.Api.Shared.Repoistories;
using Orders.Api.Shared.Repoistories.MongoDb;

namespace Orders.Api.Shared.Configuration
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
