using ualax.domain.Features.Tweet;

namespace ualax.application.Features.Tweets
{
    public interface ITweetService
    {
        Task<TweetEntity> CreateTweet(TweetEntity tweetEntity);
        Task<TweetEntity> GetTweetById(int id);
        Task<IEnumerable<TweetEntity>> GetTweetsFromUser(string username);
        Task DeleteTweetById(int id, int userId);
    }
}
