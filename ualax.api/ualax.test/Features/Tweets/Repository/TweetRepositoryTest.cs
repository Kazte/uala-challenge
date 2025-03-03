using System.Linq.Expressions;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable;
using MockQueryable.FakeItEasy;
using NuGet.Protocol.Core.Types;
using ualax.application.Abstractions.Database;
using ualax.domain.Features.Tweet;
using ualax.infrastructure.Features.Tweets;
using ualax.shared.Common;
using ualax.test.Mocks.Tweets;

namespace ualax.test.Features.Tweets.Repository
{
    public class TweetRepositoryTest
    {
        private readonly IApplicationDbContext _context;
        private readonly TweetRepository _tweetRepository;

        public TweetRepositoryTest()
        {
            _context = A.Fake<IApplicationDbContext>();
            _tweetRepository = new TweetRepository(_context);
        }

        [Fact]
        public async Task ShouldAppliesCursorAndLimit()
        {
            // arrange
            var mockDbSet = TweetsMock.AllTweets.AsQueryable().BuildMock().BuildMockDbSet();
            A.CallTo(() => _context.Tweets).Returns(mockDbSet);

            var cursor = new Cursor
            {
                Date = DateTime.UtcNow,
                Id = 3
            };

            // act
            var result = await _tweetRepository.GetTweets(null, 2, cursor);

            // assert
            result.Should().HaveCount(3)
                .And.SatisfyRespectively(
                    x => x.Id.Should().Be(4),
                    x => x.Id.Should().Be(5),
                    x => x.Id.Should().Be(3)
                );
        }

        [Fact]
        public async Task ReturnsUserTweets_WhenUserFilter()
        {
            // arrange
            var userId = 100;
            var mockDbSet = TweetsMock.AllTweets.AsQueryable().BuildMock().BuildMockDbSet();
            A.CallTo(() => _context.Tweets).Returns(mockDbSet);
            Func<IQueryable<TweetEntity>, IQueryable<TweetEntity>> filter = q => q.Where(x => x.UserId == userId);

            // act
            var result = await _tweetRepository.GetTweets(filter, 10);

            // assert
            result.Should().HaveCount(2);
            result.Should().OnlyContain(x => x.UserId == userId);
        }

        [Fact]
        public async Task ReturnTweets_WhenLimit()
        {
            // arrange
            var tweets = TweetsMock.CreateTweetsFromUser(999, 20);
            var mockDbSet = tweets.AsQueryable().BuildMock().BuildMockDbSet();
            A.CallTo(() => _context.Tweets).Returns(mockDbSet);

            var limit = 10;

            // act
            var result = await _tweetRepository.GetTweets(null, limit);

            // assert
            result.Should().HaveCount(limit + 1);
        }
    }
}
