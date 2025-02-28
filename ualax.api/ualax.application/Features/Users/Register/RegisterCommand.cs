using MediatR;

namespace ualax.application.Features.Users.Register
{
    public class RegisterCommand : IRequest
    {
        public string UserName { get; set; }
    }
}
