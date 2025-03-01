using MediatR;
using ualax.application.Abstractions;
using ualax.application.Features.Timeline.Models;

namespace ualax.application.Features.Timeline.Queries.GetTimeline
{
    public class GetTimelineQuery : IRequest<Response<TimelineResponse>>
    {
        public int UserId { get; set; }
        public string? Cursor { get; set; }
        public int Limit { get; set; }
    }
}
