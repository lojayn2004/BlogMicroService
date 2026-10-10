using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Shared.JwtConfig
{
    public static class JwtServiceCollectionExtensions
    {
        public static IServiceCollection AddSharedJwtAuth(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtSection = configuration.GetSection(JwtConfiguration.SectionName);
            services.Configure<JwtConfiguration>(jwtSection);

            var options = jwtSection.Get<JwtConfiguration>()
                ?? throw new InvalidOperationException("JWT settings missing.");

            const string schemeName = "BlogServiceJwt";

            services
                .AddAuthentication(schemeName)
                .AddJwtBearer(schemeName, o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = options.Issuer,
                        ValidAudience = options.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(options.Key))
                    };
                });

            services.AddAuthorization();
            return services;
        }
    }
}
