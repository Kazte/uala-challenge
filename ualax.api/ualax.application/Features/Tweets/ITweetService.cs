using ualax.domain.Features.Tweet;

namespace ualax.application.Features.Tweets
{
    public interface ITweetService
    {
        Task<TweetEntity> CreateTweet(TweetEntity tweetEntity);
        Task<TweetEntity> GetTweetById(int id);
        Task<IEnumerable<TweetEntity>> GetTweetsFromUser(int id);
        Task DeleteTweet(int id, int userId);
    }
}
