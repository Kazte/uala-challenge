using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ualax.application.Features.Tweets;
using ualax.domain.Features.Tweet;

namespace ualax.infrastructure.Features.Tweets
{
    public class TweetService : ITweetsService
    {
        public Task<TweetEntity> CreateTweet(TweetEntity tweetEntity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTweetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TweetEntity> GetTweetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TweetEntity>> GetTweetsFromUser(string username)
        {
            throw new NotImplementedException();
        }
    }
}
