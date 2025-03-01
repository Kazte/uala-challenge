using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ualax.application.Abstractions;

namespace ualax.application.Features.Tweets.Commands.DeleteTweet
{
    public class DeleteTweetCommand : IRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }
}
