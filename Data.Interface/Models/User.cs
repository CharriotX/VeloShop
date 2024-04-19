using Data.Interface.Models.enums;

namespace Data.Interface.Models
{
    public class User : BaseModel
    {
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public SiteRole Role { get; set; }
        public Token? Token { get; set; }
    }
}
