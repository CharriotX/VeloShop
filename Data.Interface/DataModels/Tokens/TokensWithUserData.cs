using Data.Interface.DataModels.Users;

namespace Data.Interface.DataModels.Tokens
{
    public class TokensWithUserData
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public ProfileData UserData { get; set; }
    }
}
