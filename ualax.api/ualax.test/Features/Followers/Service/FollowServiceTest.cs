using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using ualax.application.Abstractions.Exceptions;
using ualax.application.Features.Users;
using ualax.domain.Features.Follow;
using ualax.infrastructure.Features.Followers;

namespace ualax.test.Features.Followers.Service
{
    public class FollowServiceTest
    {
        private readonly IFollowRepository _followRepository;
        private readonly IUserService _userService;
        private readonly FollowService _followService;

        public FollowServiceTest()
        {
            _followRepository = A.Fake<IFollowRepository>();
            _userService = A.Fake<IUserService>();
            _followService = new FollowService(_followRepository, _userService);
        }


        [Fact]
        public async Task WhenFollowedExistsAndNotFollowing_ExpectFollow()
        {
            // arrange
            var followerId = 1;
            var followedId = 2;
            A.CallTo(() => _userService.IsUserExists(A<int>._)).Returns(true);
            A.CallTo(() => _followRepository.IsFollowing(A<int>._, A<int>._)).Returns(false);

            // act
            await _followService.Follow(followerId, followedId);

            // assert
            A.CallTo(() => _followRepository.Follow(A<FollowEntity>
                .That.Matches(x =>
                    x.FollowerId == followerId &&
                    x.FollowedId == followedId)
                )).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Follow_ThrowsError_WhenFollowedNotExists()
        {
            // arrange
            var followerId = 1;
            var followedId = 2;
            A.CallTo(() => _userService.IsUserExists(A<int>._)).Returns(false);
            A.CallTo(() => _followRepository.IsFollowing(A<int>._, A<int>._)).Returns(false);

            // act
            var result = async () => await _followService.Follow(1, 2);

            // assert
            await result.Should().ThrowAsync<NotFoundException>();

            A.CallTo(() => _userService.IsUserExists(A<int>._))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => _followRepository.IsFollowing(A<int>._, A<int>._))
                .MustNotHaveHappened();

            A.CallTo(() => _followRepository.Follow(A<FollowEntity>._))
                .MustNotHaveHappened();
        }

        [Fact]
        public async Task ThrowsError_WhenAlreadyFollowing()
        {
            // arrange
            var followerId = 1;
            var followedId = 2;
            A.CallTo(() => _userService.IsUserExists(A<int>._)).Returns(true);
            A.CallTo(() => _followRepository.IsFollowing(A<int>._, A<int>._)).Returns(true);

            // act
            var result = async () => await _followService.Follow(1, 2);

            // assert
            await result.Should().ThrowAsync<ApiException>();

            A.CallTo(() => _userService.IsUserExists(A<int>._))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => _followRepository.IsFollowing(A<int>._, A<int>._))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => _followRepository.Follow(A<FollowEntity>._))
                .MustNotHaveHappened();
        }

        [Fact]
        public async Task WhenFollowedNotExistsAndAlreadyFollowing_ExpectUnfollow()
        {
            // arrange
            var followerId = 1;
            var followedId = 2;
            A.CallTo(() => _userService.IsUserExists(A<int>._)).Returns(true);
            A.CallTo(() => _followRepository.IsFollowing(A<int>._, A<int>._)).Returns(true);
            A.CallTo(() => _followRepository.Unfollow(A<FollowEntity>._)).Returns(Task.FromResult(true));

            // act
            var result = async () => await _followService.Unfollow(followerId, followedId);

            // assert
            await result.Should().NotThrowAsync();

            A.CallTo(() => _followRepository.Unfollow(A<FollowEntity>
                .That.Matches(x =>
                    x.FollowerId == followerId &&
                    x.FollowedId == followedId)
                )).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Unfollow_ThrowsError_WhenFollowedNotExists()
        {
            // arrange
            var followerId = 1;
            var followedId = 2;
            A.CallTo(() => _userService.IsUserExists(A<int>._)).Returns(false);
            A.CallTo(() => _followRepository.IsFollowing(A<int>._, A<int>._)).Returns(true);

            // act
            var result = async () => await _followService.Unfollow(1, 2);

            // assert
            await result.Should().ThrowAsync<NotFoundException>();

            A.CallTo(() => _userService.IsUserExists(A<int>._))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => _followRepository.IsFollowing(A<int>._, A<int>._))
                .MustNotHaveHappened();

            A.CallTo(() => _followRepository.Unfollow(A<FollowEntity>._))
                .MustNotHaveHappened();
        }

        [Fact]
        public async Task ThrowsError_WhenNotFollowing()
        {
            // arrange
            var followerId = 1;
            var followedId = 2;
            A.CallTo(() => _userService.IsUserExists(A<int>._)).Returns(true);
            A.CallTo(() => _followRepository.IsFollowing(A<int>._, A<int>._)).Returns(false);

            // act
            var result = async () => await _followService.Unfollow(1, 2);

            // assert
            await result.Should().ThrowAsync<ApiException>();

            A.CallTo(() => _userService.IsUserExists(A<int>._))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => _followRepository.IsFollowing(A<int>._, A<int>._))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => _followRepository.Unfollow(A<FollowEntity>
                .That.Matches(x =>
                    x.FollowerId == followerId &&
                    x.FollowedId == followedId)
                )).MustNotHaveHappened();
        }

        [Fact]
        public async Task ThrowsError_WhenUnfollowFailed()
        {
            // arrange
            var followerId = 1;
            var followedId = 2;
            A.CallTo(() => _userService.IsUserExists(A<int>._)).Returns(true);
            A.CallTo(() => _followRepository.IsFollowing(A<int>._, A<int>._)).Returns(false);
            A.CallTo(() => _followRepository.Unfollow(A<FollowEntity>._)).Returns(Task.FromResult(false));

            // act
            var result = async () => await _followService.Unfollow(1, 2);

            // assert
            await result.Should().ThrowAsync<ApiException>();

            A.CallTo(() => _userService.IsUserExists(A<int>._))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => _followRepository.IsFollowing(A<int>._, A<int>._))
                .MustHaveHappenedOnceExactly();
        }
    }
}
