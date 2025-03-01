using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ualax.application.Features.Follows
{
    public interface IFollowService
    {
        Task Follow(int followerId, int followedId);
        Task Unfollow(int followerId, int followedId);
    }
}
