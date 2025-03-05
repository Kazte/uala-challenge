using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ualax.application.Abstractions.Authentication;
using ualax.application.Abstractions.Database;
using ualax.application.Features.Follows;
using ualax.application.Features.Timeline;
using ualax.application.Features.Tweets;
using ualax.application.Features.Users;
using ualax.domain.Features.Follow;
using ualax.domain.Features.Tweet;
using ualax.domain.Features.User;
using ualax.infrastructure.Authentication;
using ualax.infrastructure.Database;
using ualax.infrastructure.Features.Followers;
using ualax.infrastructure.Features.Timeline;
using ualax.infrastructure.Features.Tweets;
using ualax.infrastructure.Features.Users;

namespace ualax.infrastructure
{
    public static class ServiceCollections
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");

            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

            services.AddScoped<IApplicationDbContext>(x => x.GetRequiredService<ApplicationDbContext>());

            return services;
        }

        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
        {
            services.AddSingleton<IHasher, Base64Hasher>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITweetService, TweetService>();
            services.AddScoped<IFollowService, FollowService>();
            services.AddScoped<ITimelineService, TimelineService>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITweetRepository, TweetRepository>();
            services.AddScoped<IFollowRepository, FollowRepository>();

            return services;
        }
    }
}
