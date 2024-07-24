using Data.Interface.DataModels.Users;
using Data.Interface.Repositories;
using Data.Services.Interfaces.UsersService;

namespace Data.Services.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;


        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> IsEmailExist(string email)
        {
            return await _userRepository.IsEmailExist(email);
        }

        public async Task<CurrentUserData> GetUserById(int id)
        {
            var user = await _userRepository.Get(id);

            return new CurrentUserData
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                Role = user.Role.ToString()
            };
        }
        public async Task<UserWithRefreshTokenData> GetUserByUsername(string username)
        {
            var userData = await _userRepository.GetByUsername(username);

            return new UserWithRefreshTokenData
            {
                UserId = userData.Id,
                Email = userData.Email,
                Username = userData.Username,
                RefreshToken = userData.Token.RefreshToken,
                Role = userData.Role.ToString()
            };
        }
    }
}
