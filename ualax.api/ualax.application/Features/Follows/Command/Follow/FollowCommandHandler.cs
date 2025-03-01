using MediatR;
using ualax.application.Abstractions.Exceptions;

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
            if (request.FollowerId.Equals(request.FollowedId))
            {
                throw new ApiException("You can't follow yourself");
            }

            await _followService.Follow(request.FollowerId, request.FollowedId);
        }
    }
}
