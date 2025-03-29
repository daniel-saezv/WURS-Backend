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
        services.AddOptions<UserCreateOptions>().BindConfiguration("UserCreateOptions").ValidateDataAnnotations().ValidateOnStart();

        return services;
    }
}
