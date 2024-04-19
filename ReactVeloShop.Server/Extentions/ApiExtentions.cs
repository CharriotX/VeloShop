using Data.Interface.Models.enums;
using Data.Services.Interfaces.UsersService;
using Data.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using ReactVeloShop.Server.Helpers.Authorization;
using ReactVeloShop.Server.Helpers.Jwt;
using System.Text;

namespace ReactVeloShop.Server.Extentions
{
    public static class ApiExtentions
    {
        public static void AddApiAuthentication(IServiceCollection services,
            IOptions<JwtOptions> jwtOptions)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireSignedTokens = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });


            services.AddScoped<IAuthorizationHandler, RoleAuthorizationHandler>();

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("Admin", policy => policy.RequireRole(SiteRole.Admin.ToString()));
                opt.AddPolicy("User", policy => policy.RequireRole(SiteRole.User.ToString(), SiteRole.Admin.ToString()));
            });
        }
    }
}
