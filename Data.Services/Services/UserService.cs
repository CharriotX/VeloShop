﻿using Data.Interface.DataModels.Users;
using Data.Interface.Repositories;
using Data.Services.Interfaces.UsersService;
using System.Data;

namespace Data.Services.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;


        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsEmailExist(string email)
        {
            return _userRepository.IsEmailExist(email);
        }

        public CurrentUserData GetUserById(int id)
        {
            var userData = _userRepository.Get(id);

            return new CurrentUserData
            {
                Id = userData.Id,
                Email = userData.Email,
                Username = userData.Username,
                Role = userData.Role
            };
        }
        public UserWithRefreshTokenData GetUserByUsername(string username)
        {
            var userData = _userRepository.GetByUsername(username);

            return new UserWithRefreshTokenData
            {
                UserId = userData.Id,
                Email = userData.Email,
                Username = userData.Username,
                RefreshToken = userData.RefreshToken,
                Role = userData.Role
            };
        }
    }
}
