using Microsoft.AspNetCore.Authorization;

namespace energy_utility_platform_api.Middleware.Auth
{
    public class PrivilegeRequirement : IAuthorizationRequirement
    {
        public string Role { get; set; }

        public PrivilegeRequirement(string role) { Role = role; }
    }
}
