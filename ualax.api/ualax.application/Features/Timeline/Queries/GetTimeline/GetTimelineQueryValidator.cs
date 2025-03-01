using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ualax.application.Features.Timeline.Queries.GetTimeline
{
    public class GetTimelineQueryValidator : AbstractValidator<GetTimelineQuery>
    {
        public GetTimelineQueryValidator()
        {
            RuleFor(x => x.Limit).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }

}
