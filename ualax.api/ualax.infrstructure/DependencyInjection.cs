using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ualax.application.Abstractions.Authentication;
using ualax.application.Abstractions.Database;
using ualax.application.Features.Tweets;
using ualax.application.Features.Users;
using ualax.domain.Features.Tweet;
using ualax.domain.Features.User;
using ualax.infrastructure.Authentication;
using ualax.infrastructure.Database;
using ualax.infrastructure.Features.Tweets;
using ualax.infrastructure.Features.Users;

namespace ualax.infrastructure
{
    public static class DependencyInjection
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
            services.AddSingleton<IHasher, UsernameHasher>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services) {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITweetsService, ITweetsService>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITweetRepository, TweetRepository>();

            return services;
        }
    }
}
