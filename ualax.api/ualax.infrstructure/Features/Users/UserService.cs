using Microsoft.Extensions.Logging;
using ualax.application.Abstractions.Authentication;
using ualax.application.Abstractions.Exceptions;
using ualax.application.Features.Users;
using ualax.domain.Features.User;

namespace ualax.infrastructure.Features.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IHasher _hasher;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger, IHasher hasher)
        {
            _userRepository = userRepository;
            _logger = logger;
            _hasher = hasher;
        }

        public async Task<bool> IsUserExists(int id)
        {
            var user = await _userRepository.GetById(id);

            return user is null ? false : true;
        }

        public async Task<string> LoginUser(string username)
        {
            var user = await _userRepository.GetByUsername(username);

            if (user is null)
            {
                throw new NotFoundException($"User with username {username} not found");
            }

            return _hasher.Hash(user.Id.ToString());
        }

        public async Task RegisterUser(string username)
        {
            var user = await _userRepository.GetByUsername(username);

            if (user is not null)
            {
                throw new ApiException("User already created");
            }

            await _userRepository.Add(new UserEntity { Username = username }); ;
        }
    }
}
