using AutoMapper;
using MediatR;
using ualax.application.Abstractions;
using ualax.application.Features.Timeline.Models;
using ualax.application.Features.Tweets.Models;
using ualax.domain.Abstractions;

namespace ualax.application.Features.Timeline.Queries.GetTimeline
{
    public class GetTimelineQueryHandler : IRequestHandler<GetTimelineQuery, Response<TimelineResponse>>
    {
        private readonly ITimelineService _timelineService;
        private readonly IMapper _mapper;

        public GetTimelineQueryHandler(ITimelineService timelineService, IMapper mapper)
        {
            _timelineService = timelineService;
            _mapper = mapper;
        }

        public async Task<Response<TimelineResponse>> Handle(GetTimelineQuery request, CancellationToken cancellationToken)
        {
            var cursor = Cursor.FromCursor(request.Cursor);

            var tweets = await _timelineService.GetTimelineFromUser(request.UserId, request.Limit, cursor);

            var items = tweets.Select(_mapper.Map<TweetResponse>).ToList();

            DateTime? nextDate = items.Count > request.Limit ? items[^1].CreatedAt : null;
            int? nextId = items.Count > request.Limit ? items[^1].Id : null;
            items.RemoveAt(items.Count - 1);

            return new Response<TimelineResponse>(new TimelineResponse
            {
                Tweets = items,
                NextCursor = nextDate is not null && nextId is not null ? Cursor.ToCursor(nextDate.Value, nextId.Value) : null,
            });
        }
    }
}
