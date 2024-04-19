using Data.Interface.DataModels.Tokens;
using Data.Interface.DataModels.Users;

namespace Data.Services.Interfaces.AuthService
{
    public interface IAuthService
    {
        void Logout(string refreshToken);
        GeneretedTokensWithUserData Refresh(string refreshToken);
    }
}