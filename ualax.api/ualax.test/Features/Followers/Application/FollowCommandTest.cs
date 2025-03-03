using FakeItEasy;
using FluentAssertions;
using ualax.application.Abstractions.Exceptions;
using ualax.application.Features.Follows;
using ualax.application.Features.Follows.Command.Follow;
using ualax.application.Features.Users.Login;

namespace ualax.test.Features.Followers.Application
{
    public class FollowCommandTest
    {
        private readonly IFollowService _followService;
        private readonly FollowCommandHandler _followCommandHandler;

        public FollowCommandTest()
        {
            _followService = A.Fake<IFollowService>();
            _followCommandHandler = new FollowCommandHandler(_followService);
        }

        private readonly FollowCommandValidator _validator = new FollowCommandValidator();

        [Theory]
        [InlineData(0, false)]
        [InlineData(-10, false)]
        [InlineData(10, true)]
        public void FollowCommand_Validation(int followedId, bool expected)
        {
            // arrange
            var command = new FollowCommand { FollowerId = 1, FollowedId = followedId };

            // act
            var result = _validator.Validate(command);

            // assert
            result.IsValid.Should().Be(expected);
        }

        [Fact]
        public async Task CallsService_WhenValidRequest()
        {
            // arrange
            var command = new FollowCommand { FollowerId = 1, FollowedId = 2 };

            // act
            await _followCommandHandler.Handle(command, CancellationToken.None);

            // assert
            A.CallTo(() => _followService.Follow(1, 2)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ThrowsError_WhenSameId()
        {
            // arrange
            var command = new FollowCommand { FollowerId = 1, FollowedId = 1 };

            // act
            Func<Task> act = async () => await _followCommandHandler.Handle(command, CancellationToken.None);

            // assert
            await act.Should().ThrowAsync<ApiException>();

            A.CallTo(() => _followService.Follow(1, 1)).MustNotHaveHappened();
        }
    }
}
