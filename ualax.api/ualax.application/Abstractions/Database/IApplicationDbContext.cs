using Microsoft.EntityFrameworkCore;
using ualax.domain.Features.Follow;
using ualax.domain.Features.Tweet;
using ualax.domain.Features.User;

namespace ualax.application.Abstractions.Database
{
    public interface IApplicationDbContext
    {
        DbSet<UserEntity> Users { get; }
        DbSet<FollowEntity> Follows { get; }
        DbSet<TweetEntity> Tweets { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
