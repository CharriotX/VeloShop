using Data.Interface.Models.enums;

namespace Data.Interface.DataModels.Users
{
    public class CurrentUserData
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public SiteRole Role { get; set; }
    }
}
