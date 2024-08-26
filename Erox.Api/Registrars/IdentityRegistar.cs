
using Erox.Application.Optionss;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Erox.Api.Registrars
{
    public class IdentityRegistar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var jwtSettings = new JWTSettings();
            builder.Configuration.Bind(nameof(JWTSettings),jwtSettings);

            var jwtSection=builder.Configuration.GetSection(nameof(JWTSettings));
            builder.Services.Configure<JWTSettings>(jwtSection);

           

            builder.Services  
                .AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(jwt =>
            {
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SigningKey)),
                    ValidateIssuer=true,
                    ValidIssuer=jwtSettings.Issuer,
                    ValidateAudience=true,
                    ValidAudiences=jwtSettings.Audiences,
                    RequireExpirationTime=false,
                    ValidateLifetime=true
                };
                jwt.Audience = jwtSettings.Audiences[0];
                jwt.ClaimsIssuer = jwtSettings.Issuer;
                //cookies
                jwt.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["AuthToken"];
                        return Task.CompletedTask;
                    }
                };
            });
            //example
            //builder.Services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("DepartamentPolicy", policy => policy.RequireClaim("departament"));
            //});
        }
    }
}
