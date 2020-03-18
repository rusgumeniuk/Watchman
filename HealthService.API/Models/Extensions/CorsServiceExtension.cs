using Microsoft.Extensions.DependencyInjection;

namespace HealthServices.API.Models.Extensions
{
    public static class CorsServiceExtension
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                  builder => builder
                  .AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials());
            });

        }
    }
}
