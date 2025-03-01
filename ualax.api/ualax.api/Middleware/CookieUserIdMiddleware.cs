using ualax.application.Abstractions.Authentication;

namespace ualax.api.Middleware
{
    public class CookieUserIdMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHasher _hasher;

        public CookieUserIdMiddleware(RequestDelegate next, IHasher hasher)
        {
            _next = next;
            _hasher = hasher;
        }

        public async Task Invoke(HttpContext ctx)
        {
            if (ctx.Request.Cookies.TryGetValue("userId", out var userIdBase64))
            {
                var userId = _hasher.Unhash(userIdBase64);

                ctx.Items["UserId"] = userId;
            }

            await _next(ctx);
        }
    }
}
