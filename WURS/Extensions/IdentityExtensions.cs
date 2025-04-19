using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WURS.Business.Configuration.Models;
using WURS.Constants;
using WURS.Infrastructure.Contexts;

namespace WURS.Extensions;

public static class IdentityExtensions
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration config)
    {
        services.AddIdentityApiEndpoints<IdentityUser>(options =>
        {
            options.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<IdentityContext>();

        var cookieAuthOptions = config.GetRequiredValue<CookieAuthOptions>(CookieAuthOptions.SectionName);

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = cookieAuthOptions.Name;
            options.Cookie.HttpOnly = cookieAuthOptions.HttpOnly;
            options.Cookie.SecurePolicy = cookieAuthOptions.SecurePolicy;
            options.Cookie.SameSite = cookieAuthOptions.SameSite;
            options.ExpireTimeSpan = cookieAuthOptions.ExpireTimeSpan;
            options.SlidingExpiration = cookieAuthOptions.SlidingExpiration;
        });

        services.AddDbContext<IdentityContext>(options =>
        {
            var connString = config.GetConnectionString(ConfigurationSections.DefaultDbConnection);
            options.UseNpgsql(connString);
        });

        return services;
    }
}
