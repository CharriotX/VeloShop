using Data.Interface.DataModels.Tokens;
using Data.Interface.DataModels.Users;
using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Sql.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(WebContext webContext) : base(webContext)
        {
        }

        public bool IsUsernameExist(string username)
        {
            return _dbSet.Any(x => x.Username == username);
        }
        public bool IsEmailExist(string email)
        {
            return _dbSet.Any(x => x.Email == email);
        }

        public UserData GetByEmail(string email)
        {
            var user = _dbSet.Where(x => x.IsActive).FirstOrDefault(x => x.Email == email);
            var userData = new UserData
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Role = user.Role
            };

            return userData;
        }

        public UserData GetById(int id)
        {
            var user = _dbSet.Where(x => x.IsActive).FirstOrDefault(x => x.Id == id);
            var userData = new UserData
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Role = user.Role
            };

            return userData;
        }

        public UserData GetByUsername(string username)
        {
            var user = _dbSet.Where(x => x.IsActive).Include(x => x.Token).FirstOrDefault(x => x.Username == username);
            var userData = new UserData
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Role = user.Role,
                RefreshToken = user.Token.RefreshToken
            };

            return userData;
        }

        public CurrentUserData Create(UserData data)
        {
            var user = new User()
            {
                Username = data.Username,
                Email = data.Email,
                PasswordHash = data.PasswordHash,
                Role = data.Role,
                IsActive = true
            };

            Add(user);
            _webContext.SaveChanges();

            return new CurrentUserData
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username
            };
        }
    }
}
