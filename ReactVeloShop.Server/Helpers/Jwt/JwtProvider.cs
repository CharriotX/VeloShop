using Data.Interface.DataModels.Tokens;
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

        public Task<GeneretedTokensData> GenerateTokens(UserData user)
        {
            throw new NotImplementedException();
        }

        public ClaimsPrincipal ValidateAccessJwtToken(string token)
        {
            throw new NotImplementedException();
        }

        public ClaimsPrincipal ValidateRefreshJwtToken(string token)
        {
            throw new NotImplementedException();
        }
    }

}
