using Microsoft.Extensions.Options;
using WURS.Business.Configuration.Models;

namespace WURS.Middlewares;

public class UserCreateMiddleware(IOptions<UserCreateOptions> options, RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    private readonly UserCreateOptions _options = options.Value;

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("User-Create-Secret", out var secret) || secret != _options.Secret)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        await _next(context);
    }
}