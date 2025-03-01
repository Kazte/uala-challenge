using MediatR;
using Microsoft.AspNetCore.Mvc;
using ualax.api.Models.Follow;
using ualax.application.Abstractions;
using ualax.application.Features.Follows.Command.Follow;
using ualax.application.Features.Follows.Command.Unfollow;

namespace ualax.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FollowController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Follow([FromBody] FollowRequest request)
        {
            if (HttpContext.Items.TryGetValue("UserId", out var userId))
            {
                await _mediator.Send(new FollowCommand { FollowerId = int.Parse(userId.ToString()), FollowedId = request.FollowedId });
                return Ok();
            }

            return Unauthorized(new Response("Unauthorized", false));
        }

        [HttpDelete]
        public async Task<IActionResult> Unfollow([FromBody] FollowRequest request)
        {
            if (HttpContext.Items.TryGetValue("UserId", out var userId))
            {
                await _mediator.Send(new UnfollowCommand { FollowerId = int.Parse(userId.ToString()), FollowedId = request.FollowedId });
                return Ok();
            }

            return Unauthorized(new Response("Unauthorized", false));
        }
    }
}
