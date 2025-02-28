using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ualax.domain.Features.User;

namespace ualax.infrastructure.Features.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Username).IsUnique();

            builder.HasMany(u => u.Followed)
                     .WithOne(f => f.Follower)
                     .HasForeignKey(f => f.FollowerId);

            builder.HasMany(u => u.Followers)
                .WithOne(f => f.Followed)
                .HasForeignKey(f => f.FollowedId);
        }
    }
}
