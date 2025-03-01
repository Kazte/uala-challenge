using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ualax.application.Abstractions.Database;
using ualax.domain.Features.Follow;
using ualax.domain.Features.Tweet;

namespace ualax.infrastructure.Features.Followers
{
    public class FollowRepository : IFollowRepository
    {
        private readonly IApplicationDbContext _context;

        public FollowRepository(IApplicationDbContext context)
        {
            _context = context;
        }


        public async Task Follow(FollowEntity follow)
        {
            _context.Follows.Add(follow);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<int>> GetFollowersIds(int userId)
        {
            return await _context.Follows.Where(x => x.FollowerId == userId).Select(x => x.FollowedId).ToListAsync();
        }

        public async Task<bool> IsFollowing(int followerId, int followedId)
        {
            return await _context.Follows.AnyAsync(x => x.FollowerId == followerId && x.FollowedId == followedId);
        }

        public async Task<bool> Unfollow(FollowEntity follow)
        {
            var res = _context.Follows.Remove(follow);
            var isDeleted = res.State == EntityState.Deleted;
            await _context.SaveChangesAsync();

            return isDeleted;
        }
    }
}
