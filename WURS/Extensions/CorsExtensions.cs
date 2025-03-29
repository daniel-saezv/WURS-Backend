using Microsoft.Extensions.Configuration;
using WURS.Constants;

namespace WURS.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration config)
    {
        var allowedOrigins = config.GetRequiredValue<string[]>("Cors:AllowedOrigins");
        var allowedMethods = config.GetRequiredValue<string[]>("Cors:AllowedMethods");
        var allowedHeaders = config.GetRequiredValue<string[]>("Cors:AllowedHeaders");

        return services.AddCors(options =>
        {
            options.AddPolicy(ConfigurationSections.CorsSection, builder =>
            {
                builder.WithOrigins(allowedOrigins)
                       .WithMethods(allowedMethods)
                       .WithHeaders(allowedHeaders);
            });
        });
    }
}
