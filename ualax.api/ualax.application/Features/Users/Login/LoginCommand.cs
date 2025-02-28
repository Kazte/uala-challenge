using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ualax.application.Abstractions;

namespace ualax.application.Features.Users.Login
{
    public class LoginCommand : IRequest<string>
    {
        public string UserName { get; set; }
    }
}
