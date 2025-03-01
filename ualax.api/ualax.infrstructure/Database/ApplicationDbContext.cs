using Microsoft.EntityFrameworkCore;
using ualax.application.Abstractions.Database;
using ualax.domain.Features.Follow;
using ualax.domain.Features.Tweet;
using ualax.domain.Features.User;
using ualax.infrastructure.Features.Followers;
using ualax.infrastructure.Features.Tweets;
using ualax.infrastructure.Features.Users;

namespace ualax.infrastructure.Database
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<FollowEntity> Follows { get; set; }

        public DbSet<TweetEntity> Tweets { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ModelConfiguration(modelBuilder);
        }

        private static void ModelConfiguration(ModelBuilder modelBuilder) {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new FollowConfiguration());
            modelBuilder.ApplyConfiguration(new TweetConfiguration());
        }
    }
}
