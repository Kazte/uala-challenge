using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ualax.domain.Features.Follow;

namespace ualax.infrastructure.Features.Followers
{
    public class FollowConfiguration : IEntityTypeConfiguration<FollowEntity>
    {
        public void Configure(EntityTypeBuilder<FollowEntity> builder)
        {
            builder.ToTable("follows");

            builder.HasKey(f => new { f.FollowerId, f.FollowedId });
        }
    }
}
