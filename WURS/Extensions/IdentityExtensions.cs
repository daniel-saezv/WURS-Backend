using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WURS.Constants;
using WURS.Infrastructure;

namespace WURS.Extensions;

public static class IdentityExtensions
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityApiEndpoints<IdentityUser>()
                    .AddEntityFrameworkStores<IdentityContext>();

        services.AddDbContext<IdentityContext>(options =>
        {
            var connString = configuration.GetConnectionString(ConfigurationSections.DefaultDbConnection);
            options.UseNpgsql(connString);
        });

        return services;
    }
}
