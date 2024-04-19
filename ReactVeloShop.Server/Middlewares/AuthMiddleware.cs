using Data.Services.Interfaces.UsersService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ReactVeloShop.Server.Helpers.Jwt;
using System.IdentityModel.Tokens.Jwt;
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
            var authorizeHeaders = context.Request.Headers["Authorization"].FirstOrDefault();

            if (authorizeHeaders.IsNullOrEmpty())
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return _next(context);
            }

            var accessToken = context.Request.Headers["Authorization"].FirstOrDefault().Split(" ").Last();

            if (accessToken.IsNullOrEmpty())
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return _next(context);
            }

            var principal = jwtProvider.ValidateAccessJwtToken(accessToken);

            if (principal == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return _next(context);
            }

            attachUserToContext(context, userService, accessToken, options);

            //if (!authorizeHeaders.IsNullOrEmpty())
            //{
            //    var jwt = context.Request.Headers["Authorization"].FirstOrDefault().Split(" ").Last();

            //    if (jwt != null)
            //    {
            //        await attachUserToContext(context, userService, jwt, options);
            //    }
            //} else
            //{
            //    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //}

            return _next(context);
        }

        private async Task attachUserToContext(HttpContext context, IUserService userService, string token, IOptions<JwtOptions> options)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(options.Value.SecretKey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "Id").Value);

                context.Items["UserData"] = userService.GetUserById(userId);
            }
            catch
            {
                await context.ForbidAsync();
            }
        }
    }
}