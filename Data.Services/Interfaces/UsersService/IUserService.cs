using Data.Interface.DataModels.Tokens;
using Data.Interface.DataModels.Users;
using Data.Interface.Models.enums;

namespace Data.Services.Interfaces.UsersService
{
    public interface IUserService
    {
        void Register(string username, string email, string password);
        void Register(string username, string email, string password, SiteRole role);
        GeneretedTokensWithUserData Login(string email, string password);
        CurrentUserData GetUserById(int id);
    }
}
