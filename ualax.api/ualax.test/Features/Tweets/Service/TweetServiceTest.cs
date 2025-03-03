using FakeItEasy;
using FluentAssertions;
using ualax.application.Abstractions.Exceptions;
using ualax.domain.Features.Tweet;
using ualax.domain.Features.User;
using ualax.infrastructure.Features.Tweets;

namespace ualax.test.Features.Tweets.Service
{
    public class TweetServiceTest
    {
        private readonly ITweetRepository _tweetRepository;
        private readonly TweetService _tweetService;

        public TweetServiceTest()
        {
            _tweetRepository = A.Fake<ITweetRepository>();
            _tweetService = new TweetService(_tweetRepository);
        }

        [Fact]
        public async Task ReturnsTweetEntity_WhenTweetCreated()
        {
            // arrange
            var tweetEntityInput = new TweetEntity
            {
                UserId = 1,
                Content = "Hello, World!"
            };

            var tweetEntity = new TweetEntity
            {
                Id = 1,
                UserId = tweetEntityInput.UserId,
                Content = tweetEntityInput.Content,
                CreatedAt = DateTime.UtcNow
            };

            A.CallTo(() => _tweetRepository.CreateTweet(tweetEntityInput))
                .Returns(Task.FromResult<TweetEntity>(tweetEntity));

            // act
            var result = await _tweetService.CreateTweet(tweetEntityInput);

            // assert
            result.Should().BeEquivalentTo(tweetEntity);

            A.CallTo(() => _tweetRepository.CreateTweet(tweetEntityInput))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task DeletesTweet_WhenUserIsOk()
        {
            // arrange
            var tweetId = 1;
            var userId = 1;
            var tweet = new TweetEntity
            {
                Id = tweetId,
                UserId = userId
            };

            A.CallTo(() => _tweetRepository.GetTweetById(tweetId))
                .Returns(Task.FromResult<TweetEntity>(tweet));

            A.CallTo(() => _tweetRepository.DeleteTweet(tweet))
                .Returns(Task.FromResult<bool>(true));

            // act
            var result = async () => await _tweetService.DeleteTweet(tweetId, userId);

            // assert
            await result.Should().NotThrowAsync();

            A.CallTo(() => _tweetRepository.GetTweetById(tweetId))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => _tweetRepository.DeleteTweet(tweet))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ThrowException_WhenTweetIsNull()
        {
            // arrange
            var tweetId = 1;
            var userId = 1;

            A.CallTo(() => _tweetRepository.GetTweetById(tweetId))
                .Returns(Task.FromResult<TweetEntity?>(null));

            // act
            var result = async () => await _tweetService.DeleteTweet(tweetId, userId);

            // assert
            await result.Should().ThrowAsync<NotFoundException>();

            A.CallTo(() => _tweetRepository.GetTweetById(tweetId))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => _tweetRepository.DeleteTweet(A<TweetEntity>._))
                .MustNotHaveHappened();
        }

        [Fact]
        public async Task ThrowException_WhenUserIsNotOwner()
        {
            // arrange
            var tweetId = 1;
            var userId = 1;
            var tweet = new TweetEntity
            {
                Id = tweetId,
                UserId = 2
            };
            A.CallTo(() => _tweetRepository.GetTweetById(tweetId))
                .Returns(Task.FromResult<TweetEntity>(tweet));

            // act
            var result = async () => await _tweetService.DeleteTweet(tweetId, userId);

            // assert
            await result.Should().ThrowAsync<ForbiddenAccessException>();
            A.CallTo(() => _tweetRepository.GetTweetById(tweetId))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _tweetRepository.DeleteTweet(A<TweetEntity>._))
                .MustNotHaveHappened();
        }

        [Fact]
        public async Task ThrowException_WhenDeleteFails()
        {
            // arrange
            var tweetId = 1;
            var userId = 1;
            var tweet = new TweetEntity
            {
                Id = tweetId,
                UserId = userId
            };
            A.CallTo(() => _tweetRepository.GetTweetById(tweetId))
                .Returns(Task.FromResult(tweet));
            A.CallTo(() => _tweetRepository.DeleteTweet(tweet))
                .Returns(Task.FromResult(false));

            // act
            var result = async () => await _tweetService.DeleteTweet(tweetId, userId);

            // assert
            await result.Should().ThrowAsync<ApiException>();
            A.CallTo(() => _tweetRepository.GetTweetById(tweetId))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _tweetRepository.DeleteTweet(tweet))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ReturnsTweet_WhenExists()
        {
            // arrange
            var tweetId = 1;
            var tweet = new TweetEntity
            {
                Id = tweetId,
                UserId = 1,
                Content = "Hello, World!"
            };
            A.CallTo(() => _tweetRepository.GetTweetById(tweetId))
                .Returns(Task.FromResult<TweetEntity>(tweet));

            // act
            var result = await _tweetService.GetTweetById(tweetId);

            // assert
            result.Should().BeEquivalentTo(tweet);
            A.CallTo(() => _tweetRepository.GetTweetById(tweetId))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ThrowError_WhenTweetNotExists()
        {
            // arrange
            var tweetId = 1;

            A.CallTo(() => _tweetRepository.GetTweetById(tweetId))
                .Returns(Task.FromResult<TweetEntity?>(null));

            // act
            var result = async () => await _tweetService.GetTweetById(tweetId);

            // assert
            await result.Should().ThrowAsync<NotFoundException>();

            A.CallTo(() => _tweetRepository.GetTweetById(tweetId))
                .MustHaveHappenedOnceExactly();
        }
    }
}
