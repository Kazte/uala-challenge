using MediatR;
using ualax.application.Abstractions;
using ualax.application.Features.Tweets.Models;

namespace ualax.application.Features.Tweets.Commands.CreateTweet;

public class CreateTweetCommand : IRequest<Response<TweetResponse>>
{
    public string Content { get; set; }
    public int UserId { get; set; }
}