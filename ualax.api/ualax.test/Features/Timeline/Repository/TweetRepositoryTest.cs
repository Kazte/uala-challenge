using FakeItEasy;
using FluentAssertions;
using MockQueryable;
using MockQueryable.FakeItEasy;
using ualax.application.Abstractions.Database;
using ualax.domain.Features.Tweet;
using ualax.infrastructure.Features.Tweets;
using ualax.shared.Common;
using ualax.test.Mocks.Tweets;

namespace ualax.test.Features.Timeline.Repository
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
    }
}
