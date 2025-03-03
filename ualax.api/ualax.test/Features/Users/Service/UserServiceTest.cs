using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Logging;
using ualax.application.Abstractions.Authentication;
using ualax.application.Abstractions.Exceptions;
using ualax.domain.Features.User;
using ualax.infrastructure.Features.Users;

namespace ualax.test.Features.Users.Service
{
    public class UserServiceTest
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IHasher _hasher;
        private readonly UserService _userService;

        public UserServiceTest()
        {
            _userRepository = A.Fake<IUserRepository>();
            _logger = A.Fake<ILogger<UserService>>();
            _hasher = A.Fake<IHasher>();
            _userService = new UserService(_userRepository, _logger, _hasher);
        }
        [Fact]
        public async Task ReturnsTrue_WhenUserExists()
        {
            // Arrange
            var id = 1;
            var user = new UserEntity { Id = id };

            A.CallTo(() => _userRepository.GetById(id)).Returns(Task.FromResult<UserEntity>(user));

            // Act
            var result = await _userService.IsUserExists(id);

            // Assert
            result.Should().BeTrue();
            A.CallTo(() => _userRepository.GetById(id))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ReturnsFalse_WhenUserNotExists()
        {
            // arrange
            var id = 1;
            UserEntity? user = null;

            A.CallTo(() => _userRepository.GetById(id)).Returns(Task.FromResult<UserEntity>(user));

            // act
            var result = await _userService.IsUserExists(id);

            // assert
            result.Should().BeFalse();
            A.CallTo(() => _userRepository.GetById(id))
                .MustHaveHappenedOnceExactly();
        }
        [Fact]
        public async Task ReturnsHasehId_WhenUserExists()
        {
            // arrange
            var id = 1;
            var username = "userTest";
            var hashedId = "hashId";

            A.CallTo(() => _userRepository.GetByUsername(username)).Returns(Task.FromResult(new UserEntity { Id = id }));
            A.CallTo(() => _hasher.Hash(id.ToString())).Returns(hashedId);

            // act
            var result = await _userService.LoginUser(username);

            // assert
            result.Should().Be(hashedId);
            A.CallTo(() => _userRepository.GetByUsername(username))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _hasher.Hash(id.ToString()))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ThrowsNotFound_WhenUserNotExists()
        {
            // arrange
            var id = 1;
            var username = "userTest";
            var hashedId = "hashId";

            A.CallTo(() => _userRepository.GetByUsername(username)).Returns(Task.FromResult<UserEntity?>(null));
            A.CallTo(() => _hasher.Hash(id.ToString())).Returns(hashedId);

            // act
            var result = async () => await _userService.LoginUser(username);

            // assert
            await result.Should().ThrowAsync<NotFoundException>();
            A.CallTo(() => _userRepository.GetByUsername(username))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => _hasher.Hash(id.ToString()))
                .MustNotHaveHappened();
        }
        [Fact]
        public async Task ShouldThrowError_WhenUserExists()
        {
            // arrange
            var id = 1;
            var username = "userTest";

            A.CallTo(() => _userRepository.GetByUsername(username))
                .Returns(Task.FromResult(new UserEntity { Id = id }));

            // act
            var result = async () => await _userService.RegisterUser(username);

            // assert
            await result.Should().ThrowAsync<ApiException>();

            A.CallTo(() => _userRepository.GetByUsername(username))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => _userRepository.Add(A<UserEntity>
                .That.Matches(x => x.Username == username)))
                .MustNotHaveHappened();
        }

        [Fact]
        public async Task AddsNewUser_WhenUserNotExists()
        {
            // arrange
            var username = "userTest";

            A.CallTo(() => _userRepository.GetByUsername(username))
                .Returns(Task.FromResult<UserEntity?>(null));

            // act
            await _userService.RegisterUser(username);

            // assert
            A.CallTo(() => _userRepository.GetByUsername(username))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => _userRepository.Add(A<UserEntity>
                .That.Matches(x => x.Username == username)))
                .MustHaveHappenedOnceExactly();
        }
    }
}
