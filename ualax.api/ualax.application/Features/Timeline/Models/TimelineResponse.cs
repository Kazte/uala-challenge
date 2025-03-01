using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ualax.application.Features.Tweets.Models;

namespace ualax.application.Features.Timeline.Models
{
    public class TimelineResponse
    {
        public List<TweetResponse> Tweets { get; set; }
        public string? NextCursor { get; set; }
    }
}
