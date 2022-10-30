using energy_utility_platform_api.Middleware.Auth;
using energy_utility_platform_api.ViewModels;

namespace energy_utility_platform_api.Interfaces.ServiceInterfaces
{
    public interface IAuthService
    {
        public LoginViewModel Authenticate(string username, string password);

        public AuthorizationSettings GetAuthSettings();
    }
}
