using ualax.api.Middleware;

namespace ualax.api.Extensions
{
    public static class AppMiddlewareExtensions
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }

        public static void UseCookieUserIdMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CookieUserIdMiddleware>();
        }
    }
}