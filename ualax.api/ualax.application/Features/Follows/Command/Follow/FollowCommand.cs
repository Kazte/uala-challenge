using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ualax.application.Features.Follows.Command.Follow
{
    public class FollowCommand : IRequest
    {
        public int FollowerId { get; set; }
        public int FollowedId { get; set; }
    }
}
