using WURS.Middlewares;

namespace WURS.Extensions;

public static class MiddlewareExtensions
{
    public static WebApplication UseCustomMiddlewares(this WebApplication app)
    {
        app.UseWhen(context => context.Request.Path.StartsWithSegments("/register") && context.Request.Method.Equals("POST"),
                    appbuilder => appbuilder.UseMiddleware<UserCreateMiddleware>());

        return app;
    }
}
