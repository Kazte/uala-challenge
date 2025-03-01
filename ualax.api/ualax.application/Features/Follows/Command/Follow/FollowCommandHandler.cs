using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ualax.application.Features.Follows.Command.Follow
{
    public class FollowCommandHandler : IRequestHandler<FollowCommand>
    {
        private readonly IFollowService _followService;

        public FollowCommandHandler(IFollowService followService)
        {
            _followService = followService;
        }

        public async Task Handle(FollowCommand request, CancellationToken cancellationToken)
        {
            await _followService.Follow(request.FollowerId, request.FollowedId);
        }
    }
}
