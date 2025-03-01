using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ualax.application.Abstractions
{
    public class Response
    {
        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("errors")]
        public List<string>? Errors { get; set; }

        public Response() { }

        public Response(string message, bool isSuccess)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }

    public class Response<T> : Response
    {
        [JsonPropertyName("data")]
        public T? Data { get; set; }

        public Response()
        {
        }

        public Response(T data, string message = null)
            : base(message, true)
        {
            Data = data;
        }
    }
}