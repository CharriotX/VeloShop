using Data.Services.Interfaces.UsersService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ReactVeloShop.Server.Helpers.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReactVeloShop.Server.Middlewares
{
    public class AuthMiddleware
    {
        private RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public Task Invoke(HttpContext context, IUserService userService, IOptions<JwtOptions> options, IJwtProvider jwtProvider)
        {
            var authHeader = context.Request.Headers.ContainsKey("autorization");

            if (context.Request.Headers.ContainsKey("autorization"))
            {
                var token = context.Request.Headers["autorization"].FirstOrDefault().Split(" ").Last();
                attachUserToContext(context, userService, token, options);
            }

            return _next(context);
        }

        private async Task attachUserToContext(HttpContext context, IUserService userService, string token, IOptions<JwtOptions> options)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(options.Value.SecretKey);
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var username = jwtToken.Claims.First(x => x.Type == ClaimTypes.Name).Value;
                var userData = userService.GetUserByUsername(username);

                context.User = principal;
            }
            catch
            {
                await context.ForbidAsync();
            }
        }
    }
}