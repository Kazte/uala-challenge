using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ualax.domain.Features.Tweet;

namespace ualax.domain.Features.User
{
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<TweetEntity> Tweets { get; set; } = new List<Tweet>();

        public ICollection<Follow> Followers { get; set; } = new List<Follow>();

        public ICollection<Follow> Followed { get; set; } = new List<Follow>();
    }
}
