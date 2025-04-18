using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WURS.Constants;
using WURS.Infrastructure.Contexts;

namespace WURS.Extensions;

public static class IdentityExtensions
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityApiEndpoints<IdentityUser>(options =>
        {
            options.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<IdentityContext>();

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = "WURS.Identity";
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.ExpireTimeSpan = TimeSpan.FromDays(1);
            options.SlidingExpiration = true;
        });

        services.AddDbContext<IdentityContext>(options =>
        {
            var connString = configuration.GetConnectionString(ConfigurationSections.DefaultDbConnection);
            options.UseNpgsql(connString);
        });

        return services;
    }
}
