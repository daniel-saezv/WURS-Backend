using Microsoft.Extensions.Configuration;

namespace WURS.Extensions;

public static class ConfigurationExtensions
{
    public static T GetRequiredValue<T>(this IConfiguration configuration, string key)
    {
        var value = configuration.GetSection(key).Get<T>();

        if (value is null)
        {
            throw new InvalidOperationException($"Configuration key '{key}' is missing or has a null value.");
        }

        if (value is string stringValue && string.IsNullOrWhiteSpace(stringValue))
        {
            throw new InvalidOperationException($"Configuration key '{key}' is missing, has a null value, has an empty or whitespace value.");
        }

        return value;
    }
}
