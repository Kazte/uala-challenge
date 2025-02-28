using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ualax.application.Abstractions.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException()
        : base()
        {
        }

        public NotFoundException(string message) : base(404, message) { }

        public NotFoundException(string name, object key)
            : base(404, $"Entity not found \"{name}\" ({key}).")
        {
        }
    }
}
