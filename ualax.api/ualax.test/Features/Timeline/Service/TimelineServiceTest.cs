using FakeItEasy;
using FluentAssertions;
using ualax.domain.Features.Follow;
using ualax.domain.Features.Tweet;
using ualax.infrastructure.Features.Timeline;

namespace ualax.test.Features.Timeline.Service
{
    public class TimelineServiceTest
    {
        private readonly IFollowRepository _followRepository;
        private readonly ITweetRepository _tweetRepository;
        private readonly TimelineService _timelineService;

        public TimelineServiceTest()
        {
            _followRepository = A.Fake<IFollowRepository>();
            _tweetRepository = A.Fake<ITweetRepository>();
            _timelineService = new TimelineService(_followRepository, _tweetRepository);
        }


        [Fact]
        public async Task ReturnsTweetsFromFollowedUsers()
        {
            // arrange
            var userId = 1;
            var limit = 10;
            var followers = new List<int> { 2, 5 };
            var tweets = new List<TweetEntity>
                {
                    new TweetEntity { Id = 1, UserId = 2 },
                    new TweetEntity { Id = 2, UserId = 5 },
                    new TweetEntity { Id = 3, UserId = 3 },
                    new TweetEntity { Id = 4, UserId = 4 }
                };



            A.CallTo(() => _followRepository.GetFollowersIds(userId))
                .Returns(Task.FromResult<IEnumerable<int>>(followers));

            A.CallTo(() => _tweetRepository.GetTweets(
                A<Func<IQueryable<TweetEntity>,
                IQueryable<TweetEntity>>>.Ignored,
                limit,
                null))
                .ReturnsLazily(valueProducer =>
                {
                    var filter = valueProducer.Arguments.Get<Func<IQueryable<TweetEntity>, IQueryable<TweetEntity>>>(0);
                    return Task.FromResult(filter(tweets.AsQueryable()).AsEnumerable());
                });

            // act
            var result = await _timelineService.GetTimelineFromUser(userId, limit, null);

            // assert
            result.Should().HaveCount(2)
                .And.Contain(t => followers.Contains(t.UserId))
                .And.NotContain(t => !followers.Contains(t.UserId));

            A.CallTo(() => _followRepository.GetFollowersIds(userId))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => _tweetRepository.GetTweets(
                A<Func<IQueryable<TweetEntity>,
                IQueryable<TweetEntity>>>.Ignored,
                limit,
                null))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ReturnsEmptyList_WhenNoFollowers()
        {
            // arrange
            var userId = 1;
            var limit = 10;
            var followers = new List<int>();
            var tweets = new List<TweetEntity>
                {
                    new TweetEntity { Id = 1, UserId = 2 },
                    new TweetEntity { Id = 2, UserId = 5 },
                    new TweetEntity { Id = 3, UserId = 3 },
                    new TweetEntity { Id = 4, UserId = 4 }
                };


            A.CallTo(() => _followRepository.GetFollowersIds(userId))
                .Returns(Task.FromResult<IEnumerable<int>>(followers));

            A.CallTo(() => _tweetRepository.GetTweets(
                A<Func<IQueryable<TweetEntity>,
                IQueryable<TweetEntity>>>.Ignored,
                limit,
                null))
                .ReturnsLazily(valueProducer =>
                {
                    var filter = valueProducer.Arguments.Get<Func<IQueryable<TweetEntity>, IQueryable<TweetEntity>>>(0);

                    return Task.FromResult(filter(tweets.AsQueryable()).AsEnumerable());
                });

            // act
            var result = await _timelineService.GetTimelineFromUser(userId, limit, null);

            // assert
            result.Should().BeEmpty();

            A.CallTo(() => _followRepository.GetFollowersIds(userId))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => _tweetRepository.GetTweets(
                A<Func<IQueryable<TweetEntity>,
                IQueryable<TweetEntity>>>.Ignored,
                limit,
                null))
                .MustHaveHappenedOnceExactly();
        }
    }
}
