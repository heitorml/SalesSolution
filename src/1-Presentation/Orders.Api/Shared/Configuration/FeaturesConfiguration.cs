using Orders.Api.Features.OrdersResale;
using Orders.Api.Features.OrdersSupplier;

namespace Orders.Api.Shared.Configuration
{
    public static class FeaturesConfiguration
    {
        public static void AddFeatures(this IServiceCollection services)
        {

            services.AddScoped<ICreateOrderResalesFeature, CreateOrderResalesFeture>();
            services.AddScoped<ICreateOrderSupplierFeature, CreateOrderSupplierFeature>();
        }
    }
}
