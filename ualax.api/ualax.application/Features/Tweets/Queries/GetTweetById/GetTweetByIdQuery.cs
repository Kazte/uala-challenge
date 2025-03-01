using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ualax.application.Abstractions;
using ualax.application.Features.Tweets.Models;

namespace ualax.application.Features.Tweets.Queries.GetTweetById
{
    public class GetTweetByIdQuery : IRequest<Response<TweetResponse>>
    {
        public int Id { get; set; }
        public GetTweetByIdQuery(int id)
        {
            Id = id;
        }
    }
}