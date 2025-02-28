using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ualax.domain.Features.Tweet
{
    public interface ITweetRepository
    {
        public Task<TweetEntity> CreateTweet(TweetEntity tweetEntity);

        public Task<bool> DeleteTweetById(TweetEntity tweetEntity);

        public Task<TweetEntity?> GetTweetById(int id);

        public Task<IEnumerable<TweetEntity>> GetTweetsFromUser(string username);
    }
}
