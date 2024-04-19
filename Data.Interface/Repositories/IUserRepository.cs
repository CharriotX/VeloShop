﻿using Data.Interface.DataModels.Tokens;
using Data.Interface.DataModels.Users;
using Data.Interface.Models;

namespace Data.Interface.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        CurrentUserData Create(UserData data);
        UserData GetByEmail(string email);
        UserData GetByUsername(string username);
        bool IsUsernameExist(string username);
        bool IsEmailExist(string email);
        void SaveToken(int userId, string token);
        GeneretedTokensData GetTokens(int userId);
    }
}
