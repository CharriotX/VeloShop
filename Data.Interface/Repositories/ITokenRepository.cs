using Data.Interface.DataModels.Tokens;
using Data.Interface.DataModels.Users;

namespace Data.Interface.Repositories
{
    public interface ITokenRepository
    {
        Task RemoveToken(string refreshToken);
        Task<TokensData> SetToken(int userId, string refreshToken);
        Task<UserWithRefreshTokenData> GetTokenWithUserId(string refreshToken);
        Task SaveToken(int userId, string token);
        Task<TokensData> GetTokens(int userId);
    }
}
