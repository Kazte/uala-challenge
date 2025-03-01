using MediatR;
using ualax.application.Abstractions.Exceptions;

namespace ualax.application.Features.Follows.Command.Unfollow
{
    public class UnfollowCommandHandler : IRequestHandler<UnfollowCommand>
    {
        private readonly IFollowService _followService;

        public UnfollowCommandHandler(IFollowService followService)
        {
            _followService = followService;
        }

        public async Task Handle(UnfollowCommand request, CancellationToken cancellationToken)
        {
            if (request.FollowerId.Equals(request.FollowedId))
            {
                throw new ApiException("You can't unfollow yourself");
            }

            await _followService.Unfollow(request.FollowerId, request.FollowedId);
        }
    }
}
