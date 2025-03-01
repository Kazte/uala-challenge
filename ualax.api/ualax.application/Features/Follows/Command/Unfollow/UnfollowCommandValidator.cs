using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ualax.application.Features.Follows.Command.Unfollow
{
    public class UnfollowCommandValidator:AbstractValidator<UnfollowCommand>
    {
        public UnfollowCommandValidator()
        {
            RuleFor(x => x.FollowedId).GreaterThan(0).NotEmpty();
        }
    }
}
