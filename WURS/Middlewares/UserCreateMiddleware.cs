using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
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
            IdentityResult result = IdentityResult.Failed(new IdentityError { Code = "Unauthorized", Description = "Unauthorized" });
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(result);
            return;
        }

        await _next(context);
    }

    private bool IsValidSecret(string providedSecret)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_options.Secret));
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(providedSecret));
        var providedHash = Convert.FromBase64String(providedSecret);

        return computedHash.SequenceEqual(providedHash);
    }
}