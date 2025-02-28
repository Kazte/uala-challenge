using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ualax.application.Abstractions.Database;
using ualax.domain.Features.Tweet;

namespace ualax.infrastructure.Features.Tweets
{
    public class TweetRepository : ITweetRepository
    {
        private readonly IApplicationDbContext _context;

        public TweetRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TweetEntity> CreateTweet(TweetEntity tweetEntity)
        {
            var e = _context.Tweets.Add(tweetEntity);
            await _context.SaveChangesAsync();
            return e.Entity;
        }

        public async Task<bool> DeleteTweetById(TweetEntity tweetEntity)
        {
            var res = _context.Tweets.Remove(tweetEntity);
            await _context.SaveChangesAsync();

            return res.State == EntityState.Deleted;
        }

        public async Task<TweetEntity?> GetTweetById(int id)
        {
            return await _context.Tweets.AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<IEnumerable<TweetEntity>> GetTweetsFromUser(string username)
        {
            return await _context.Tweets.Where(x => x.User.Username.Equals(username)).AsNoTracking().ToListAsync();
        }
    }
}
