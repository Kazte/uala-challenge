using MediatR;
using Microsoft.AspNetCore.Mvc;
using ualax.api.Models.Tweet;
using ualax.application.Abstractions;
using ualax.application.Features.Tweets.Commands.CreateTweet;
using ualax.application.Features.Tweets.Commands.DeleteTweet;
using ualax.application.Features.Tweets.Queries.GetTweetById;

namespace ualax.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TweetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTweetById(int id)
        {
            var tweet = await _mediator.Send(new GetTweetByIdQuery(id));

            return Ok(tweet);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTweet([FromBody] CreateTweetRequest tweetRequest)
        {

            if (HttpContext.Items.TryGetValue("UserId", out var userId))
            {
                var tweet = await _mediator.Send(new CreateTweetCommand { Content = tweetRequest.Content, UserId = int.Parse(userId.ToString()) });
                return CreatedAtAction(nameof(CreateTweet), new { id = tweet.Data.Id }, tweet);
            }

            return Unauthorized(new Response("Unauthorized", false));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTweet(int id)
        {
            if (HttpContext.Items.TryGetValue("UserId", out var userId))
            {
                await _mediator.Send(new DeleteTweetCommand { Id = id, UserId = int.Parse(userId.ToString()) });
                return Ok();
            }

            return Unauthorized(new Response("Unauthorized", false));
        }
    }
}
