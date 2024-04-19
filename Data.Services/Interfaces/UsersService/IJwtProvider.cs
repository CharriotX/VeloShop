using Data.Interface.DataModels.Tokens;
using Data.Interface.DataModels.Users;
using System.Security.Claims;

namespace Data.Services.Interfaces.UsersService
{
    public interface IJwtProvider
    {
        Task<GeneretedTokensData> GenerateTokens(UserData user);
        ClaimsPrincipal ValidateAccessJwtToken(string token);
        ClaimsPrincipal ValidateRefreshJwtToken(string token);
    }
}