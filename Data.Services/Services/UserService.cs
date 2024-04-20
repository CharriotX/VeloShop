using Data.Interface.DataModels.Tokens;
using Data.Interface.DataModels.Users;
using Data.Interface.Models;
using Data.Interface.Models.enums;
using Data.Interface.Repositories;
using Data.Services.Interfaces.UsersService;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace Data.Services.Services
{
    public class UserService : IUserService
    {
        private IPasswordHasher _passwordHasher;
        private IUserRepository _userRepository;
        private IJwtProvider _jwtProvider;
        private IHttpContextAccessor _httpContext;
        private ITokenRepository _tokenRepository;

        public UserService(IPasswordHasher passwordHasher, IUserRepository userRepository, IJwtProvider jwtProvider, IHttpContextAccessor httpContext, ITokenRepository tokenRepository)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _httpContext = httpContext;
            _tokenRepository = tokenRepository;
        }

        public GeneretedTokensData GetToken(int userId)
        {
            throw new NotImplementedException();
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

        public GeneretedTokensWithUserData Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void Register(string username, string email, string password)
        {
            var isEmailExis = _userRepository.IsEmailExist(email);

            if (isEmailExis)
            {
                throw new Exception("This email already registered");
            }

            var passwordHash = _passwordHasher.Generate(password);

            var userData = new UserData
            {
                Email = email,
                Username = username,
                PasswordHash = passwordHash
            };

            _userRepository.Create(userData);
        }

        public void Register(string username, string email, string password, SiteRole role)
        {
            var isEmailExis = _userRepository.IsEmailExist(email);

            if (isEmailExis)
            {
                throw new Exception("This email already registered");
            }

            var passwordHash = _passwordHasher.Generate(password);

            var userData = new UserData
            {
                Email = email,
                Username = username,
                PasswordHash = passwordHash,
                Role = role
            };

            _userRepository.Create(userData);

        }
    }
}
