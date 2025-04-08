using Orders.Worker.Shared.Infrastructure.MongoDb;
using Orders.Worker.Shared.Infrastructure.Repoistories;
using Orders.Worker.Shared.Infrastructure.Repoistories.MongoDb;

namespace Orders.Worker.Shared.Configuration
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
