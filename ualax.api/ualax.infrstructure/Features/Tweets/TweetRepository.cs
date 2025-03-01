using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ualax.application.Abstractions.Database;
using ualax.domain.Abstractions;
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
            var isDeleted = res.State == EntityState.Deleted;
            await _context.SaveChangesAsync();

            return isDeleted;
        }

        public async Task<TweetEntity?> GetTweetById(int id)
        {
            return await _context.Tweets.AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<IEnumerable<TweetEntity>> GetTweets(Func<IQueryable<TweetEntity>, IQueryable<TweetEntity>> filter, Func<IQueryable<TweetEntity>, IOrderedQueryable<TweetEntity>> orderBy, int limit, Cursor? cursor = null)
        {
            var sql = _context.Tweets.AsQueryable();

            if (filter != null)
            {
                sql = filter(sql);
            }

            if (orderBy != null)
            {
                sql = orderBy(sql);
            }

            if (cursor != null)
            {
                
                sql = sql.Where(x => EF.Functions.LessThanOrEqual(
                    ValueTuple.Create(x.CreatedAt, x.Id),
                    ValueTuple.Create(cursor.Date, cursor.Id)));
            }

            var res = await sql.Take(limit + 1).Include(x => x.User).AsNoTracking().ToListAsync();

            return res;
        }

        public async Task<IEnumerable<TweetEntity>> GetTweetsFromUser(string username)
        {
            return await _context.Tweets.Where(x => x.User.Username.Equals(username)).AsNoTracking().ToListAsync();
        }
    }
}
