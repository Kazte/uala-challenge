using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using ualax.application.Abstractions;
using ualax.application.Abstractions.Exceptions;

namespace ualax.api.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
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
                        _logger.LogError("API Error: Error while processing {request}:\n{result}", ctx.Request, e.Message);
                        break;

                    case ValidationException e:
                        res.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errors = e.Errors.SelectMany(x => x.Value).ToList();
                        break;

                    case DbUpdateException e:
                        res.StatusCode = (int)HttpStatusCode.InternalServerError;
                        responseModel.Message = _env.IsDevelopment() ? responseModel.Message : "Internal Server Error";
                        _logger.LogError("Database Error: {error}\n{innerException}", ex.Message, ex);
                        break;

                    default:
                        res.StatusCode = (int)HttpStatusCode.InternalServerError;
                        responseModel.Message = _env.IsDevelopment() ? responseModel.Message : "Internal Server Error";
                        _logger.LogError("Unhandled Error: {error}\n{innerException}", ex.Message, ex);
                        break;
                }

                var result = JsonSerializer.Serialize(responseModel);

                await res.WriteAsync(result);
            }
        }
    }
}
