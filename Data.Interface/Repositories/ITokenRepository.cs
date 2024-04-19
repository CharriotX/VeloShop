using Data.Interface.DataModels.Tokens;
using Data.Interface.DataModels.Users;

namespace Data.Interface.Repositories
{
    public interface ITokenRepository
    {
        void RemoveToken(string refreshToken);
        GeneretedTokensData SetToken(int userId, string refreshToken);
        UserWithRefreshTokenData GetTokenWithUserId(string refreshToken);
    }
}