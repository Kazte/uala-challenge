using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ualax.domain.Features.Tweet;

namespace ualax.infrastructure.Features.Tweets
{
    public class TweetConfiguration : IEntityTypeConfiguration<TweetEntity>
    {
        public void Configure(EntityTypeBuilder<TweetEntity> builder)
        {
            builder.ToTable("tweets");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Tweets)
                .HasForeignKey(x => x.UserId);
        }
    }
}
