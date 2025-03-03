using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ualax.domain.Features.Tweet;
using ualax.shared.Common;

namespace ualax.application.Features.Timeline
{
    public interface ITimelineService
    {
        Task<List<TweetEntity>> GetTimelineFromUser(int userId, int limit, Cursor? cursor);
    }
}
