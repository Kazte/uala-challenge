using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ualax.application.Abstractions;
using ualax.application.Features.Follows.Command.Follow;
using ualax.application.Features.Timeline.Queries.GetTimeline;

namespace ualax.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimelineController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TimelineController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetTimeline(string? cursor, int limit = 10)
        {
            if (HttpContext.Items.TryGetValue("UserId", out var userId))
            {
                var tweetsResponse = await _mediator.Send(new GetTimelineQuery
                {
                    UserId = int.Parse(userId.ToString()),
                    Cursor = cursor,
                    Limit = limit
                });
                return Ok(tweetsResponse);
            }

            return Unauthorized(new Response("Unauthorized", false));
        }
    }
}
