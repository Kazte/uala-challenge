﻿using ualax.shared.Common;

namespace ualax.domain.Features.Tweet
{
    public interface ITweetRepository
    {
        public Task<TweetEntity> CreateTweet(TweetEntity tweetEntity);

        public Task<bool> DeleteTweet(TweetEntity tweetEntity);

        public Task<TweetEntity?> GetTweetById(int id);
        public Task<IEnumerable<TweetEntity>> GetTweets(
            Func<IQueryable<TweetEntity>, IQueryable<TweetEntity>> filter,
            int limit,
            Cursor? cursor = null);
    }
}
