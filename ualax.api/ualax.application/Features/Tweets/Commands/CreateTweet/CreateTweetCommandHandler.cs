using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using ualax.application.Abstractions;
using ualax.application.Features.Tweets.Models;
using ualax.domain.Features.Tweet;

namespace ualax.application.Features.Tweets.Commands.CreateTweet;

public class CreateTweetCommandHandler : IRequestHandler<CreateTweetCommand, Response<TweetResponse>>
{
    private readonly ITweetService _tweetService;
    private readonly IMapper _mapper;

    public CreateTweetCommandHandler(ITweetService tweetService, IMapper mapper)
    {
        _tweetService = tweetService;
        _mapper = mapper;
    }

    public async Task<Response<TweetResponse>> Handle(CreateTweetCommand request, CancellationToken cancellationToken)
    {
        var response = await _tweetService.CreateTweet(_mapper.Map<TweetEntity>(request));

        return new Response<TweetResponse>(_mapper.Map<TweetResponse>(response));
    }
}