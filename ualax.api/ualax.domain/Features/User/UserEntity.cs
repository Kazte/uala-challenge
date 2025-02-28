using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ualax.domain.Features.Follow;
using ualax.domain.Features.Tweet;

namespace ualax.domain.Features.User
{
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("username")]
        public string Username { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<TweetEntity> Tweets { get; set; } = new List<TweetEntity>();

        public ICollection<FollowEntity> Followers { get; set; } = new List<FollowEntity>();

        public ICollection<FollowEntity> Followed { get; set; } = new List<FollowEntity>();
    }
}
