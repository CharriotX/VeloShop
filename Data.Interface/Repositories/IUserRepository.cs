using Data.Interface.DataModels.Tokens;
using Data.Interface.DataModels.Users;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> IsUsernameExist(string username);
        Task<bool> IsEmailExist(string email);
        Task<CurrentUserData> Create(UserData data);
        Task<UserData> GetByEmail(string email);
        Task<UserData> GetByUsername(string username);
        Task<UserData> GetById(int id);        
    }
}
