using Microsoft.EntityFrameworkCore;
using WURS.Infrastructure.Contexts;

namespace WURS.Extensions;

public static class EfExtensions
{
    public static async Task AddContextsAsync(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var identityContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();

            await identityContext.Database.MigrateAsync();
        }
    }
}
