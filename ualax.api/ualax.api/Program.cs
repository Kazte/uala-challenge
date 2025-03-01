
using ualax.api.Extensions;
using ualax.application;
using ualax.infrastructure;

namespace ualax.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplication();
            builder.Services.AddServices();
            builder.Services.AddAuthenticationServices();
            builder.Services.AddRepositories();
            builder.Services.AddDatabase(builder.Configuration);
            builder.Services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<RouteOptions>(opt =>
            {
                opt.LowercaseUrls = true;
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCookieUserIdMiddleware();
            // custom error middleware
            app.UseErrorHandlingMiddleware();

            app.MapControllers();

            app.Run();
        }
    }
}
