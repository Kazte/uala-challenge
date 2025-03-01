using ualax.application.Abstractions.Exceptions;
using ualax.application.Features.Follows;
using ualax.application.Features.Users;
using ualax.domain.Features.Follow;

namespace ualax.infrastructure.Features.Followers
{
    public class FollowService : IFollowService
    {
        private readonly IFollowRepository _followRepository;
        private readonly IUserService _userService;

        public FollowService(IFollowRepository followRepository, IUserService userService)
        {
            _followRepository = followRepository;
            _userService = userService;
        }

        public async Task Follow(int followerId, int followedId)
        {
            var followedExists = await _userService.IsUserExists(followedId);

            if (!followedExists)
            {
                throw new NotFoundException("Followed not found");
            }

            if (await _followRepository.IsFollowing(followerId, followedId))
            {
                throw new ApiException("You are already following this user");
            }

            await _followRepository.Follow(new FollowEntity
            {
                FollowerId = followerId,
                FollowedId = followedId
            });
        }

        public async Task Unfollow(int followerId, int followedId)
        {
            var followedExists = await _userService.IsUserExists(followedId);

            if (!followedExists)
            {
                throw new NotFoundException("Followed not found");
            }

            if (!await _followRepository.IsFollowing(followerId, followedId))
            {
                throw new ApiException("You are not following this user");
            }

            var isUnfollow = await _followRepository.Unfollow(new FollowEntity
            {
                FollowerId = followerId,
                FollowedId = followedId
            });

            if (!isUnfollow)
            {
                throw new ApiException("Failed to unfollow user");
            }
        }
    }
}
