using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ualax.application.Abstractions;
using ualax.application.Features.Users.Register;

namespace ualax.application.Features.Users.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Response<string>>
    {
        private readonly ILogger<LoginCommandHandler> _logger;
        private readonly IUserService _userService;

        public LoginCommandHandler(ILogger<LoginCommandHandler> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public async Task<Response<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            string userId = await _userService.LoginUser(request.UserName);

            return new Response<string>
            {
                Data = userId,
                Message = "Login success.",
                IsSuccess = true
            };
        }
    }
}
