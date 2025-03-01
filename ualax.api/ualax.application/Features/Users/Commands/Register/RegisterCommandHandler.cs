using MediatR;
using Microsoft.Extensions.Logging;

namespace ualax.application.Features.Users.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly ILogger<RegisterCommandHandler> _logger;
        private readonly IUserService _userService;

        public RegisterCommandHandler(ILogger<RegisterCommandHandler> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _userService.RegisterUser(request.UserName);
        }
    }
}
