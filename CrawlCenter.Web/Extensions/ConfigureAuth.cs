using System;
using System.Text;
using System.Threading.Tasks;
using CrawlCenter.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace CrawlCenter.Web.Extensions;

public static class ConfigureAuth {
    public static void AddAuth(this IServiceCollection services, IConfiguration configuration) {
        services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => {
                var secSettings = configuration.GetSection(nameof(SecuritySettings)).Get<SecuritySettings>();
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = secSettings.Token.Issuer,
                    ValidAudience = secSettings.Token.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secSettings.Token.Key)),
                    ClockSkew = TimeSpan.Zero
                };
            });
    }
}