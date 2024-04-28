using Data.Interface.DataModels.Tokens;
using Data.Interface.DataModels.Users;
using Data.Interface.Models.enums;

namespace Data.Services.Interfaces.UsersService
{
    public interface IUserService
    {
       
        CurrentUserData GetUserById(int id);
        UserWithRefreshTokenData GetUserByUsername(string username);
        bool IsEmailExist(string email);
    }
}
