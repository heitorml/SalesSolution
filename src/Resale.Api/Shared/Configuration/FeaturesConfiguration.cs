using Resales.Api.Features.Create;
using Resales.Api.Features.GetAll;
using Resales.Api.Features.GetById;
using Resales.Api.Features.Updade;

namespace Resales.Api.Shared.Configuration
{
    public static class FeaturesConfiguration
    {
        public static void AddFeatures(this IServiceCollection services)
        {
            services.AddScoped<IUpdateResaleFeature, UpdateResaleFeature>();
            services.AddScoped<IResalesCreateFeature, ResalesCreateFeature>();
            services.AddScoped<IGetAllResaleFeature, GetAllResaleFeature>();
            services.AddScoped<IGetResaleByIdFeature, GetResaleByIdFeature>();
        }
    }
}
