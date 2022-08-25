using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using YB.Todo.Filters;

namespace YB.Todo.Extentions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adds Exception, action filters, disabled ModelStateFilter.
        /// </summary>
        /// <param name="services">Instance of the services for configuration.</param>
        /// <param name="servicesOptions">Information about current service.</param>
        /// <returns>Services to proceed with configuration in builder manner.</returns>
        public static IServiceCollection AddPlatformMvc(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(PlatformHttpGlobalExceptionFilter));
                //options.Filters.Add(typeof(GlobalValidateModelActionFilter));
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }
    }
}
