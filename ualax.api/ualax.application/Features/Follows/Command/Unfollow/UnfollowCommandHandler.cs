using MediatR;

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
            await _followService.Unfollow(request.FollowerId, request.FollowedId);
        }
    }
}
