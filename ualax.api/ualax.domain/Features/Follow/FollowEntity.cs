using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ualax.domain.Features.User;

namespace ualax.domain.Features.Follow
{
    public class FollowEntity
    {
        
        [Required]
        public int FollowerId { get; set; }

        [Required]
        public int FollowedId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [ForeignKey("FollowerId")]
        public UserEntity Follower { get; set; }
        
        [ForeignKey("FollowedId")]
        public UserEntity Followed { get; set; }
    }
}
