namespace Data.Interface.DataModels.Helpers
{
    public class LoginResponse
    {
        public bool IsLogedIn { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
