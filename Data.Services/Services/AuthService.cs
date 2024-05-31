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


        public async Task<TokensWithUserData> Login(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);
            var verifyPass = _passwordHasher.Verify(password, user.PasswordHash);

            if (verifyPass == false)
            {
                return null;
            }

            var userData = new UserData
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role,
            };

            var tokens = _jwtProvider.GenerateTokens(userData);

            var newTokens = await _tokenRepository.SetToken(userData.Id, tokens.RefreshToken);

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

        public async Task Register(RegisterUserData data)
        {
            var passwordHash = _passwordHasher.Generate(data.Password);

            var userData = new UserData
            {
                Email = data.Email,
                Username = data.Username,
                PasswordHash = passwordHash,
                Role = SiteRole.User
            };

            await _userRepository.Create(userData);
        }

        public async Task Register(RegisterUserData data, SiteRole role)
        {
            var passwordHash = _passwordHasher.Generate(data.Password);

            var userData = new UserData
            {
                Email = data.Email,
                Username = data.Username,
                PasswordHash = passwordHash,
                Role = role
            };

            await _userRepository.Create(userData);
        }

        public  async Task<TokensWithUserData> UpdateRefreshToken(string username, string refreshToken)
        {
            var user = await _userRepository.GetByUsername(username);
            var savedUserToken = user.Token.RefreshToken;

            if (refreshToken != savedUserToken)
            {
                return null;
            }

            var userData = new UserData
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role,
            };

            var newJwtTokens = _jwtProvider.GenerateTokens(userData);

            if (newJwtTokens == null)
            {
                return null;
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

            await _tokenRepository.RemoveToken(refreshToken);
            await _tokenRepository.SetToken(userData.Id, newJwtTokens.RefreshToken);

            return tokenAndUserData;
        }
    }
}
