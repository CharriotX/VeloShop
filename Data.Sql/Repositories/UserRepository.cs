﻿using Data.Interface.DataModels.Tokens;
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

        public async Task<bool> IsUsernameExist(string username)
        {
            return await _dbSet.AnyAsync(x => x.Username == username);
        }
        public async Task<bool> IsEmailExist(string email)
        {
            return await _dbSet.AnyAsync(x => x.Email == email);
        }

        public async Task<UserData> GetByEmail(string email)
        {
            var user = await _dbSet.Where(x => x.IsActive).FirstOrDefaultAsync(x => x.Email == email);
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

        public async Task<UserData> GetById(int id)
        {
            var user = await _dbSet.Where(x => x.IsActive).FirstOrDefaultAsync(x => x.Id == id);
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

        public async Task<UserData> GetByUsername(string username)
        {
            var user = await _dbSet.Where(x => x.IsActive).Include(x => x.Token).FirstOrDefaultAsync(x => x.Username == username);
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

        public async Task<CurrentUserData> Create(UserData data)
        {
            var user = new User()
            {
                Username = data.Username,
                Email = data.Email,
                PasswordHash = data.PasswordHash,
                Role = data.Role,
                IsActive = true
            };

            await Add(user);
            await _webContext.SaveChangesAsync();

            return new CurrentUserData
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username
            };
        }
    }
}
