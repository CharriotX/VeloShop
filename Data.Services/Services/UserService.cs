using Data.Interface.DataModels.Tokens;
using Data.Interface.DataModels.Users;
using Data.Interface.Models;
using Data.Interface.Models.enums;
using Data.Interface.Repositories;
using Data.Services.Interfaces.UsersService;
using Microsoft.AspNetCore.Http;

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
            throw new NotImplementedException();
        }

        public GeneretedTokensWithUserData Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public UserDataWithTokens Register(string username, string email, string password)
        {
            throw new NotImplementedException();
        }

        public UserDataWithTokens Register(string username, string email, string password, SiteRole role)
        {
            throw new NotImplementedException();
        }
    }
}
