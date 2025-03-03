using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ualax.application.Abstractions.Exceptions;
using ualax.application.Features.Tweets;
using ualax.domain.Features.Tweet;

namespace ualax.infrastructure.Features.Tweets
{
    public class TweetService : ITweetService
    {
        private readonly ITweetRepository _tweetRepository;

        public TweetService(ITweetRepository tweetRepository)
        {
            _tweetRepository = tweetRepository;
        }

        public async Task<TweetEntity> CreateTweet(TweetEntity tweetEntity)
        {
            return await _tweetRepository.CreateTweet(tweetEntity);
        }

        public async Task DeleteTweet(int id, int userId)
        {
            var tweet = await _tweetRepository.GetTweetById(id);

            if (tweet is null)
            {
                throw new NotFoundException($"Tweet with id {id} not found");
            }

            if (tweet.UserId != userId)
            {
                throw new ForbiddenAccessException();
            }

            var isDeleted = await _tweetRepository.DeleteTweet(tweet);

            if (!isDeleted)
            {
                throw new ApiException($"Failed to delete tweet with id {id}");
            }
        }

        public async Task<TweetEntity> GetTweetById(int id)
        {
            var tweet = await _tweetRepository.GetTweetById(id);

            if (tweet is null)
            {
                throw new NotFoundException($"Tweet with id {id} not found");
            }

            return tweet;
        }

        public Task<IEnumerable<TweetEntity>> GetTweetsFromUser(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
