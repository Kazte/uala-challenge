using System.Net;
using System.Text.Json;
using ualax.application.Abstractions;
using ualax.application.Abstractions.Exceptions;

namespace ualax.api.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext ctx)
        {
            try
            {
                await _next(ctx);
            }
            catch (Exception ex)
            {
                var res = ctx.Response;
                res.ContentType = "application/json";
                var responseModel = new Response<string>
                {
                    IsSuccess = false,
                    Message = ex?.Message
                };

                switch (ex)
                {
                    case ApiException e:
                        res.StatusCode = e.StatusCode;
                        break;

                    case ValidationException e:
                        res.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errors = e.Errors.SelectMany(x => x.Value).ToList();
                        break;

                    default:
                        res.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(responseModel);

                await res.WriteAsync(result);
            }
        }
    }
}
