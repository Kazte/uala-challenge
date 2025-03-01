using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ualax.application.Features.Follows.Command.Unfollow
{
    public class UnfollowCommand : IRequest
    {
        public int FollowerId { get; set; }
        public int FollowedId { get; set; }
    }
}
