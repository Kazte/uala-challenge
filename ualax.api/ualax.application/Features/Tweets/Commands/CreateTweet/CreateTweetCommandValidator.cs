using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ualax.application.Features.Tweets.Commands.CreateTweet
{
    public class CreateTweetCommandValidator : AbstractValidator<CreateTweetCommand>
    {
        public CreateTweetCommandValidator()
        {
            RuleFor(x => x.Content).NotEmpty().MaximumLength(140).WithMessage("Tweet content must be less than 140 characters");
        }
    }
}
