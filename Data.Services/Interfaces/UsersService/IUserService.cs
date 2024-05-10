using Data.Interface.DataModels.Tokens;
using Data.Interface.DataModels.Users;
using Data.Interface.Models.enums;

namespace Data.Services.Interfaces.UsersService
{
    public interface IUserService
    {
       
        Task<CurrentUserData> GetUserById(int id);
        Task<UserWithRefreshTokenData> GetUserByUsername(string username);
        Task<bool> IsEmailExist(string email);
    }
}
