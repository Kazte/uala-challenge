using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ualax.application.Features.Tweets.Commands.CreateTweet;
using ualax.application.Features.Tweets.Models;
using ualax.domain.Features.Tweet;

namespace ualax.application.Mappings
{
    public class TweetAutomapperProfile : Profile
    {
        public TweetAutomapperProfile()
        {
            CreateMap<TweetEntity, TweetResponse>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username));

            CreateMap<CreateTweetCommand, TweetEntity>();
        }
    }
}
