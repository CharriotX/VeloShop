﻿using Data.Interface.DataModels.Tokens;
using Data.Interface.DataModels.Users;
using Data.Services.Interfaces.UsersService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ReactVeloShop.Server.Helpers.Jwt
{
    public class JwtProvider : IJwtProvider
    {
        private JwtOptions _options;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public TokensData GenerateTokens(UserData data)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, data.Username),
                    new Claim(ClaimTypes.Role, data.Role.ToString())
                };
                var tokenKey = Encoding.UTF8.GetBytes(_options.SecretKey);
                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(12),
                    signingCredentials: credentials);

                var refreshToken = GenerateRefreshToken();
                return new TokensData { AccessToken = tokenHandler.WriteToken(token), RefreshToken = refreshToken };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];

            using var generator = RandomNumberGenerator.Create();

            generator.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal GetTokenPrincipal(string token)
        {
            var key = Encoding.UTF8.GetBytes(_options.SecretKey);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };
            
            return new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out _);
        }
    }

}
