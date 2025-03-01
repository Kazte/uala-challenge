﻿using ualax.domain.Abstractions;

namespace ualax.domain.Features.Tweet
{
    public interface ITweetRepository
    {
        public Task<TweetEntity> CreateTweet(TweetEntity tweetEntity);

        public Task<bool> DeleteTweetById(TweetEntity tweetEntity);

        public Task<TweetEntity?> GetTweetById(int id);

        public Task<IEnumerable<TweetEntity>> GetTweetsFromUser(string username);
        public Task<IEnumerable<TweetEntity>> GetTweets(
            Func<IQueryable<TweetEntity>, IQueryable<TweetEntity>> filter,
            Func<IQueryable<TweetEntity>, IOrderedQueryable<TweetEntity>> orderBy,
            int limit,
            Cursor? cursor = null);

        public Task<int> GetTweetCount(Func<IQueryable<TweetEntity>, IQueryable<TweetEntity>> filter);
    }
}
