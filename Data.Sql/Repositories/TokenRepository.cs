using Data.Interface.DataModels.Tokens;
using Data.Interface.DataModels.Users;
using Data.Interface.Models;
using Data.Interface.Repositories;

namespace Data.Sql.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private WebContext _webContext;

        public TokenRepository(WebContext webContext)
        {
            _webContext = webContext;
        }

        public void RemoveToken(string refreshToken)
        {
            var token = _webContext.Tokens.FirstOrDefault(x => x.RefreshToken == refreshToken);
            _webContext.Tokens.Remove(token);
            _webContext.SaveChanges();
        }

        public GeneretedTokensData SetToken(int userId, string refreshToken)
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

                return new GeneretedTokensData
                {
                    RefreshToken = refreshToken
                };
            }

            token.RefreshToken = refreshToken;
            _webContext.SaveChanges();

            return new GeneretedTokensData
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
    }
}
