namespace Data.Interface.DataModels.Users
{
    public class UserWithRefreshTokenData
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string RefreshToken { get; set; }
    }
}
