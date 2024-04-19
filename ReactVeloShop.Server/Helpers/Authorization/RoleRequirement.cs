using Data.Interface.Models.enums;
using Microsoft.AspNetCore.Authorization;

namespace ReactVeloShop.Server.Helpers.Authorization
{
    public class RoleRequirement(SiteRole role) : IAuthorizationRequirement
    {
        public SiteRole Role { get; set; } = role;
    }
}
