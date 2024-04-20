using Data.Interface.DataModels.Tokens;
using Data.Interface.DataModels.Users;
using Data.Interface.Models.enums;

namespace Data.Services.Interfaces.AuthService
{
    public interface IAuthService
    {
        void Logout(string refreshToken);
        GeneretedTokensWithUserData Login(string email, string password);
        GeneretedTokensWithUserData Refresh(string refreshToken);
        UserDataWithTokens Register(string username, string email, string password);
        UserDataWithTokens Register(string username, string email, string password, SiteRole role);
        GeneretedTokensData GetToken(int userId);
    }
}