namespace ualax.domain.Features.Follow
{
    public interface IFollowRepository
    {
        Task<bool> IsFollowing(int followerId, int followedId);
        Task Follow(FollowEntity follow);
        Task<bool> Unfollow(FollowEntity follow);
        Task<IEnumerable<int>> GetFollowersIds(int userId);
    }
}
