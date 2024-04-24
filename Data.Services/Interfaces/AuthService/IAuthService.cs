using Data.Interface.DataModels.Tokens;
using Data.Interface.DataModels.Users;
using Data.Interface.Models.enums;

namespace Data.Services.Interfaces.AuthService
{
    public interface IAuthService
    {
        TokensWithUserData Login(string email, string password);
        void Register(RegisterUserData data);
        void Register(RegisterUserData data, SiteRole role);
        TokensWithUserData UpdateRefreshToken(string username, string refreshToken);
        
    }
}