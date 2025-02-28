using ualax.application.Abstractions.Authentication;

namespace ualax.api.Middleware
{
    public class CookieUsernameMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHasher _hasher;

        public CookieUsernameMiddleware(RequestDelegate next, IHasher hasher)
        {
            _next = next;
            _hasher = hasher;
        }

        public async Task InvokeAsnyc(HttpContext ctx)
        {
            if (ctx.Request.Cookies.TryGetValue("username", out var usernameBase64))
            {
                var username = _hasher.Unhash(usernameBase64);

                ctx.Items["Username"] = username;
            }

            await _next(ctx);
        }
    }
}
