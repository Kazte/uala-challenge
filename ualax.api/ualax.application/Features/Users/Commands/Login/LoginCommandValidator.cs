using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace ualax.application.Features.Users.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(r => r.UserName).NotEmpty().WithMessage("Username is required").MaximumLength(50).WithMessage("Should be lesser than 50 characters");
        }
    }
}
