using Levge.ConsistentResponse.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLevgeConsistentResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddControllers(options =>
            {
                options.Filters.Add<ApiResponseFilter>();
                options.Filters.Add<ModelStateValidationFilter>();
            });

            return services;
        }
    }
}
