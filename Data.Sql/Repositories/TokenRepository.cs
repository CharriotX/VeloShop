using Data.Interface.DataModels.Tokens;
using Data.Interface.DataModels.Users;
using Data.Interface.Models;
using Data.Interface.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Sql.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private WebContext _webContext;
        private IUserRepository _userRepository;

        public TokenRepository(WebContext webContext, IUserRepository userRepository)
        {
            _webContext = webContext;
            _userRepository = userRepository;
        }

        public void RemoveToken(string refreshToken)
        {
            var token = _webContext.Tokens.FirstOrDefault(x => x.RefreshToken == refreshToken);
            _webContext.Tokens.Remove(token);
            _webContext.SaveChanges();
        }

        public TokensData SetToken(int userId, string refreshToken)
        {
            var token = _webContext.Tokens.FirstOrDefault(x => x.UserId == userId);

            if (token == null)
            {
                var newToken = new Token()
                {
                    UserId = userId,
                    RefreshToken = refreshToken
                };

                _webContext.Add(newToken);
                _webContext.SaveChanges();

                return new TokensData
                {
                    RefreshToken = refreshToken
                };
            }

            token.RefreshToken = refreshToken;
            _webContext.SaveChanges();

            return new TokensData
            {
                RefreshToken = refreshToken
            };
        }

        public UserWithRefreshTokenData GetTokenWithUserId(string refreshToken)
        {
            var token = _webContext.Tokens.FirstOrDefault(x => x.RefreshToken == refreshToken);

            if(token == null)
            {
                return null;
            }

            return new UserWithRefreshTokenData { 
                UserId = token.UserId,
                RefreshToken = token.RefreshToken
            };
        }

        public void SaveToken(int userId, string token)
        {
            var user = _userRepository.GetById(userId);
            user.RefreshToken = token;

            _webContext.SaveChanges();
        }

        public TokensData GetTokens(int userId)
        {
            var user = _userRepository.GetById(userId);

            return new TokensData
            {
                RefreshToken = user.RefreshToken
            };
        }
    }
}
