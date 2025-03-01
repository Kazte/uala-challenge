using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ualax.application.Features.Follows.Command.Follow
{
    public class FollowCommandValidator : AbstractValidator<FollowCommand>
    {
        public FollowCommandValidator()
        {
            RuleFor(x => x.FollowedId).GreaterThan(0).NotEmpty();
        }
    }
}
