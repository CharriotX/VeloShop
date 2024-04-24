using Data.Interface.DataModels.Tokens;
using Data.Interface.DataModels.Users;
using System.Security.Claims;

namespace Data.Services.Interfaces.UsersService
{
    public interface IJwtProvider
    {
        TokensData GenerateTokens(UserData data);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}