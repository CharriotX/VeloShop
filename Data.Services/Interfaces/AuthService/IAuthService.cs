using Data.Interface.DataModels.Tokens;
using Data.Interface.DataModels.Users;
using Data.Interface.Models.enums;

namespace Data.Services.Interfaces.AuthService
{
    public interface IAuthService
    {
        Task<TokensWithUserData> Login(string email, string password);
        Task Register(RegisterUserData data);
        Task Register(RegisterUserData data, SiteRole role);
        Task<TokensWithUserData> UpdateRefreshToken(string username, string refreshToken);
        
    }
}