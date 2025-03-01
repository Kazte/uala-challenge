using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ualax.application.Abstractions.Exceptions
{
    public class ApiException : Exception
    {
        public int StatusCode { get; } = 400;
        public ApiException() : base()
        {
        }

        public ApiException(string message) : base(message)
        {
        }

        public ApiException(int statusCode, string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message))
        {
            StatusCode = statusCode;
        }

        public ApiException(int statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
