using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using ualax.application.Features.Timeline;
using ualax.application.Features.Timeline.Queries.GetTimeline;
using ualax.application.Features.Tweets.Models;
using ualax.domain.Features.Tweet;
using ualax.shared.Common;
using ualax.test.Mocks.Tweets;

namespace ualax.test.Features.Timeline.Application
{
    public class GetTimelineQueryTest
    {
        private readonly ITimelineService _timelineService;
        private readonly IMapper _mapper;
        private readonly GetTimelineQueryHandler _getTimelineQueryHandler;

        public GetTimelineQueryTest()
        {
            _timelineService = A.Fake<ITimelineService>();
            _mapper = A.Fake<IMapper>();
            _getTimelineQueryHandler = new GetTimelineQueryHandler(_timelineService, _mapper);
        }

        private readonly GetTimelineQueryValidator _validator = new GetTimelineQueryValidator();

        [Theory]
        [InlineData(-1, false)]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(100, true)]
        [InlineData(101, false)]
        public void GetTimelineQuery_Validator(int limit, bool expected)
        {
            // arrange 
            var query = new GetTimelineQuery { UserId = 1, Limit = limit };

            // act
            var result = _validator.Validate(query);

            // assert
            result.IsValid.Should().Be(expected);
        }

        [Fact]
        public async Task ReturnsTweetsWithCursor_WhenMoreItemsThanLimit()
        {
            // arrange
            var request = new GetTimelineQuery { UserId = 1, Limit = 3 };
            var tweets = TweetsMock.AllTweets.Take(4).ToList();

            var expectedResponse = tweets.Select(x => new TweetResponse
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt
            }).ToList();

            A.CallTo(() => _timelineService.GetTimelineFromUser(A<int>._, A<int>._, A<Cursor>._))
                .Returns(tweets);

            A.CallTo(() => _mapper.Map<TweetResponse>(A<TweetEntity>._))
                .ReturnsNextFromSequence(expectedResponse.ToArray());


            // act
            var result = await _getTimelineQueryHandler.Handle(request, CancellationToken.None);

            // assert
            result.Data.Tweets.Should().HaveCount(3);
            result.Data.NextCursor.Should().Be(Cursor.ToCursor(
                expectedResponse[^1].CreatedAt,
                expectedResponse[^1].Id));
        }

        [Fact]
        public async Task ReturnsTweetsWithoutCursor_WhenLessItemsThanLimit()
        {
            // arrange
            var query = new GetTimelineQuery { Limit = 5 };
            var tweets = TweetsMock.AllTweets;

            A.CallTo(() => _timelineService.GetTimelineFromUser(A<int>._, A<int>._, A<Cursor>._))
                .Returns(Task.FromResult(tweets));

            A.CallTo(() => _mapper.Map<TweetResponse>(A<TweetEntity>._))
                .ReturnsNextFromSequence(tweets.Select(x => new TweetResponse
                {
                    Id = x.Id,
                    CreatedAt = x.CreatedAt
                }).ToArray());

            // act
            var result = await _getTimelineQueryHandler.Handle(query, CancellationToken.None);

            // assert
            result.Data.NextCursor.Should().BeNull();
            result.Data.Tweets.Should().HaveCount(5);
        }

        [Fact]
        public async Task UsesNullCursor_WhenInvalidCursor()
        {
            // arrange
            var query = new GetTimelineQuery { Cursor = "invalid" };

            // act
            await _getTimelineQueryHandler.Handle(query, CancellationToken.None);

            // assert
            A.CallTo(() => _timelineService.GetTimelineFromUser(A<int>._, A<int>._, null))
                .MustHaveHappenedOnceExactly();
        }
    }
}
