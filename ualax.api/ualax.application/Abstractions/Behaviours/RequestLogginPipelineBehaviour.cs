using MediatR;
using Microsoft.Extensions.Logging;

namespace ualax.application.Abstractions.Behaviours
{
    public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

        public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[REQ] Handling execution {request.GetType().FullName} with request {@request}");
            var response = await next();
            _logger.LogInformation($"[RES] Handled execution {request.GetType().FullName} with response {@response}");

            return response;
        }
    }
}
