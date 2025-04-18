using Microsoft.AspNetCore.Identity;

namespace WURS.Middlewares;

public class LoginMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var useCookies = context.Request.Query["useCookies"];

        if (useCookies.Count == 0 || !bool.TryParse(useCookies, out var value) || !value)
        {
            IdentityResult result = IdentityResult.Failed(new IdentityError { Code = "Unauthorized", Description = "Unauthorized" });
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(result);
            return;
        }

        await _next(context);
    }
}
