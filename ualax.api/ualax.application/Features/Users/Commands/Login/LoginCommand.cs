using MediatR;
using ualax.application.Abstractions;

namespace ualax.application.Features.Users.Login
{
    public class LoginCommand : IRequest<Response<string>>
    {
        public string UserName { get; set; }
    }
}
