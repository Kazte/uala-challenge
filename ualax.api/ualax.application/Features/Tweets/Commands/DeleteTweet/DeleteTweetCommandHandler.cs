using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ualax.application.Abstractions;

namespace ualax.application.Features.Tweets.Commands.DeleteTweet
{
    internal class DeleteTweetCommandHandler : IRequestHandler<DeleteTweetCommand>
    {
        private readonly ITweetService _tweetService;

        public DeleteTweetCommandHandler(ITweetService tweetService)
        {
            _tweetService = tweetService;
        }

        public async Task Handle(DeleteTweetCommand request, CancellationToken cancellationToken)
        {
            await _tweetService.DeleteTweetById(request.Id, request.UserId);
        }
    }
}
