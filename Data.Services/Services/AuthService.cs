using Data.Interface.DataModels.Tokens;
using Data.Interface.DataModels.Users;
using Data.Interface.Models;
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
        private IPasswordHasher _passwordHasher;
        public AuthService(ITokenRepository tokenRepository, IJwtProvider jwtProvider, IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _tokenRepository = tokenRepository;
            _jwtProvider = jwtProvider;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }


        public TokensWithUserData Login(string email, string password)
        {
            var userData = _userRepository.GetByEmail(email);
            var verifyPass = _passwordHasher.Verify(password, userData.PasswordHash);

            if (verifyPass == false)
            {
                throw new Exception("Wrong password!");
            }

            var tokens = _jwtProvider.GenerateTokens(userData);

            var newTokens = _tokenRepository.SetToken(userData.Id, tokens.RefreshToken);

            if (newTokens == null)
            {
                return null;
            }

            return new TokensWithUserData
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken,
                UserData = new ProfileData
                {
                    Email = userData.Email,
                    Username = userData.Username,
                }
            };

        }

        public void Register(RegisterUserData data)
        {
            var passwordHash = _passwordHasher.Generate(data.Password);

            var userData = new UserData
            {
                Email = data.Email,
                Username = data.Username,
                PasswordHash = passwordHash,
                Role = SiteRole.User
            };

            _userRepository.Create(userData);
        }

        public void Register(RegisterUserData data, SiteRole role)
        {
            var passwordHash = _passwordHasher.Generate(data.Password);

            var userData = new UserData
            {
                Email = data.Email,
                Username = data.Username,
                PasswordHash = passwordHash,
                Role = role
            };

            _userRepository.Create(userData);
        }

        public TokensWithUserData UpdateRefreshToken(string username, string refreshToken)
        {
            var userData = _userRepository.GetByUsername(username);
            var savedUserToken = userData.RefreshToken;

            if (refreshToken != savedUserToken)
            {
                throw new UnauthorizedAccessException();
            }

            var newJwtTokens = _jwtProvider.GenerateTokens(userData);

            if (newJwtTokens == null)
            {
                throw new UnauthorizedAccessException();
            }

            var tokenAndUserData = new TokensWithUserData
            {
                RefreshToken = newJwtTokens.RefreshToken,
                AccessToken = newJwtTokens.AccessToken,
                UserData = new ProfileData
                {
                    Email = userData.Email,
                    Username = userData.Username
                }
            };

            _tokenRepository.RemoveToken(refreshToken);
            _tokenRepository.SetToken(userData.Id, newJwtTokens.RefreshToken);

            return tokenAndUserData;
        }
    }
}
