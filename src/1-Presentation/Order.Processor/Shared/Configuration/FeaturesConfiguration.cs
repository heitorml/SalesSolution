using Orders.Api.Features.ShippingToSupplier;
using Orders.Worker.Features.OrderCancel;

namespace Orders.Worker.Shared.Configuration
{
    public static class FeaturesConfiguration
    {
        public static void AddFeatures(this IServiceCollection services)
        {
            services.AddScoped<IShippingToSupplierFeature, ShippingToSupplierFeature>();
            services.AddScoped<IOrderCancelFeature, OrderCancelFeature>();
        }
    }
}
