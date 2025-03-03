using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ualax.domain.Features.Tweet;

namespace ualax.test.Mocks.Tweets
{
    public static class TweetsMock
    {
        public static List<TweetEntity> AllTweets => new List<TweetEntity>
        {
            new TweetEntity
            {
                Id = 1,
                Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                UserId = 100
            },
            new TweetEntity
            {
                Id = 2,
                Content = "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                UserId = 100
            },
            new TweetEntity
            {
                Id = 3,
                Content = "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris.",
                CreatedAt = DateTime.UtcNow.AddHours(-2),
                UserId = 200
            },
            new TweetEntity
            {
                Id = 4,
                Content = "Duis aute irure dolor in reprehenderit in voluptate velit esse.",
                CreatedAt = DateTime.UtcNow,
                UserId = 300
            },
            new TweetEntity
            {
                Id = 5,
                Content = "Cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat.",
                CreatedAt = DateTime.UtcNow.AddMinutes(-30),
                UserId = 400
            }
        };

        public static List<TweetEntity> CreateTweetsFromUser(int userId, int count)
        {
            var tweets = new List<TweetEntity>();
            for (int i = 0; i < count; i++)
            {
                tweets.Add(new TweetEntity
                {
                    Id = i + 1,
                    Content = $"Tweet {i + 1} from user {userId}",
                    CreatedAt = DateTime.UtcNow.AddDays(-i),
                    UserId = userId
                });
            }
            return tweets;
        }
    }
}
