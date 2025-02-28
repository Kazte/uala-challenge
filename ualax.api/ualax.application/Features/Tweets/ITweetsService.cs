using ualax.domain.Features.Tweet;

namespace ualax.application.Features.Tweets
{
    public interface ITweetsService
    {
        Task<TweetEntity> CreateTweet(TweetEntity tweetEntity);
        Task<TweetEntity> GetTweetById(int id);
        Task<IEnumerable<TweetEntity>> GetTweetsFromUser(string username);
        Task<bool> DeleteTweetById(int id);
    }
}
