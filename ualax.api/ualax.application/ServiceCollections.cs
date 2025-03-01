using System.Reflection;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ualax.application.Abstractions.Behaviours;
using ualax.application.Mappings;

namespace ualax.application
{
    public static class ServiceCollections
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                configuration.AddOpenBehavior(typeof(LoggingPipelineBehavior<,>));
                configuration.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}