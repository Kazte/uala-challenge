using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ualax.application.Abstractions;
using ualax.application.Features.Tweets.Models;

namespace ualax.application.Features.Tweets.Queries.GetTweetById
{
    public class GetTweetByIdQueryHandler : IRequestHandler<GetTweetByIdQuery, Response<TweetResponse>>
    {
        private readonly ITweetService _tweetService;
        private readonly IMapper _mapper;

        public GetTweetByIdQueryHandler(ITweetService tweetService, IMapper mapper)
        {
            _tweetService = tweetService;
            _mapper = mapper;
        }

        public async Task<Response<TweetResponse>> Handle(GetTweetByIdQuery request, CancellationToken cancellationToken)
        {
            var tweet = await _tweetService.GetTweetById(request.Id);
            var tweetResponse = _mapper.Map<TweetResponse>(tweet);
            return new Response<TweetResponse>(tweetResponse);
        }
    }
}
