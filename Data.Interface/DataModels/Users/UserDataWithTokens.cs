using Data.Interface.DataModels.Tokens;

namespace Data.Interface.DataModels.Users
{
    public class UserDataWithTokens
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public GeneretedTokensData Tokens { get; set; }
    }
}
