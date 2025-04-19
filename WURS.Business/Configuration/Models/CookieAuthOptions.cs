using Microsoft.AspNetCore.Http;

namespace WURS.Business.Configuration.Models;

public class CookieAuthOptions
{
    public static string SectionName { get; } = nameof(CookieAuthOptions);
    public required string Name { get; init; }
    public required bool HttpOnly { get; init; }
    public required CookieSecurePolicy SecurePolicy { get; init; }
    public required SameSiteMode SameSite { get; init; }
    public required TimeSpan ExpireTimeSpan { get; init; }
    public required bool SlidingExpiration { get; init; }
}
