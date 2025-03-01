using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ualax.application.Abstractions.Exceptions
{
    public class ForbiddenAccessException : ApiException
    {
        public ForbiddenAccessException() : base(403, "You are not allowed") { }
    }
}
