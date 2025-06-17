using Levge.ConsistentResponse.Filters;
using Levge.ConsistentResponse.Transformers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLevgeConsistentResponse(this IServiceCollection services, bool lowerUrls = false)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            if (lowerUrls)
                services.Configure<RouteOptions>(options =>
                {
                    options.LowercaseUrls = true;
                });

            services.AddControllers(options =>
            {
                options.Filters.Add<ApiResponseFilter>();
                options.Filters.Add<ModelStateValidationFilter>();

                if (lowerUrls)
                    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            });

            return services;
        }
    }
}
