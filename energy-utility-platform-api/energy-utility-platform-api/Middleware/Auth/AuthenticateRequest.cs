using System.ComponentModel.DataAnnotations;

namespace energy_utility_platform_api.Middleware.Auth
{
    public class AuthenticateRequest
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set;}

    }
}
