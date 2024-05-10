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

        public async Task RemoveToken(string refreshToken)
        {
            var token = await _webContext.Tokens.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
            _webContext.Tokens.Remove(token);
            await _webContext.SaveChangesAsync();
        }

        public async Task<TokensData> SetToken(int userId, string refreshToken)
        {
            var token = await _webContext.Tokens.FirstOrDefaultAsync(x => x.UserId == userId);

            if (token == null)
            {
                var newToken = new Token()
                {
                    UserId = userId,
                    RefreshToken = refreshToken
                };

                await _webContext.AddAsync(newToken);
                await _webContext.SaveChangesAsync();

                return new TokensData
                {
                    RefreshToken = refreshToken
                };
            }

            token.RefreshToken = refreshToken;
            await _webContext.SaveChangesAsync();

            return new TokensData
            {
                RefreshToken = refreshToken
            };
        }

        public async Task<UserWithRefreshTokenData> GetTokenWithUserId(string refreshToken)
        {
            var token = await _webContext.Tokens.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);

            if(token == null)
            {
                return null;
            }

            return new UserWithRefreshTokenData { 
                UserId = token.UserId,
                RefreshToken = token.RefreshToken
            };
        }

        public async Task SaveToken(int userId, string token)
        {
            var user = await _userRepository.GetById(userId);
            user.RefreshToken = token;

            await _webContext.SaveChangesAsync();
        }

        public async Task<TokensData> GetTokens(int userId)
        {
            var user = await _userRepository.GetById(userId);

            return new TokensData
            {
                RefreshToken = user.RefreshToken
            };
        }
    }
}
