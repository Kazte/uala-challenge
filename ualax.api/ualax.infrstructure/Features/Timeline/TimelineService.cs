﻿using ualax.application.Features.Timeline;
using ualax.domain.Features.Follow;
using ualax.domain.Features.Tweet;
using ualax.shared.Common;

namespace ualax.infrastructure.Features.Timeline
{
    public class TimelineService : ITimelineService
    {
        private readonly IFollowRepository _followRepository;
        private readonly ITweetRepository _tweetRepository;

        public TimelineService(IFollowRepository followRepository, ITweetRepository tweetRepository)
        {
            _followRepository = followRepository;
            _tweetRepository = tweetRepository;
        }

        public async Task<List<TweetEntity>> GetTimelineFromUser(int userId, int limit, Cursor? cursor)
        {
            var ids = await _followRepository.GetFollowersIds(userId);

            Func<IQueryable<TweetEntity>, IQueryable<TweetEntity>> filter = query =>
                query.Where(t => ids.Contains(t.UserId));

            var tweets = await _tweetRepository.GetTweets(filter, limit, cursor);

            return tweets.ToList();
        }
    }
}
