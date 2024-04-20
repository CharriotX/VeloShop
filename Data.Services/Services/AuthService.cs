using Data.Interface.DataModels.Tokens;
using Data.Interface.DataModels.Users;
using Data.Interface.Models.enums;
using Data.Interface.Repositories;
using Data.Services.Interfaces.AuthService;
using Data.Services.Interfaces.UsersService;

namespace Data.Services.Services
{
    public class AuthService : IAuthService
    {
        private ITokenRepository _tokenRepository;
        private IUserRepository _userRepository;
        private IJwtProvider _jwtProvider;
        public AuthService(ITokenRepository tokenRepository, IJwtProvider jwtProvider, IUserRepository userRepository)
        {
            _tokenRepository = tokenRepository;
            _jwtProvider = jwtProvider;
            _userRepository = userRepository;
        }

        public GeneretedTokensData GetToken(int userId)
        {
            throw new NotImplementedException();
        }

        public GeneretedTokensWithUserData Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void Logout(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public GeneretedTokensWithUserData Refresh(string refreshToken)
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
