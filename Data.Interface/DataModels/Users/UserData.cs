using Data.Interface.Models.enums;

namespace Data.Interface.DataModels.Users
{
    public class UserData
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public SiteRole Role { get; set; }
        public string PasswordHash { get; set; }
        public string RefreshToken { get; set; }
    }
}
