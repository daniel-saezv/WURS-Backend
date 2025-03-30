using WURS.Business.Configuration.Models;

namespace WURS.Extensions;

public static class ConfigurationExtensions
{
    public static T GetRequiredValue<T>(this IConfiguration configuration, string key)
    {
        var value = configuration.GetSection(key).Get<T>() ??
            throw new InvalidOperationException($"Configuration key '{key}' is missing or has a null value.");

        if (value is string stringValue && string.IsNullOrWhiteSpace(stringValue))
        {
            throw new InvalidOperationException($"Configuration key '{key}' is missing, has a null value, has an empty or whitespace value.");
        }

        return value;
    }

    public static IServiceCollection AddCustomOptions(this IServiceCollection services)
    {
        services.AddOptions<DefaultCorsPolicy>(DefaultCorsPolicy.SectionName)
            .AddOptions<UserCreateOptions>(UserCreateOptions.SectionName);

        return services;
    }

    private static IServiceCollection AddOptions<TOptions>(
        this IServiceCollection services,
        string configKey) where TOptions : class
    {
        services.AddOptions<TOptions>()
           .BindConfiguration(configKey)
           .ValidateDataAnnotations()
           .ValidateOnStart();

        return services;
    }
}
