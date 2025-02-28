using Microsoft.AspNetCore.Mvc;

namespace ualax.application.Abstractions.Extensions
{
    public static class HttpResponseExtensions
    {
        public static void AddCookie(this ObjectResult response, string key, string value)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };
            var httpContext = new HttpContextAccessor().HttpContext;
            httpContext.Response.Cookies.Append(key, value, cookieOptions);
        }
    }
}
