using WURS.Business.Configuration.Models;

namespace WURS.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration config)
    {
        var policy = config.GetRequiredValue<DefaultCorsPolicy>(DefaultCorsPolicy.SectionName);

        return services.AddCors(options =>
        {
            options.AddPolicy(DefaultCorsPolicy.SectionName, builder =>
            {
                builder.WithOrigins(policy.AllowedOrigins)
                       .WithMethods(policy.AllowedMethods)
                       .WithHeaders(policy.AllowedHeaders)
                       .AllowCredentials();
            });
        });
    }
}
