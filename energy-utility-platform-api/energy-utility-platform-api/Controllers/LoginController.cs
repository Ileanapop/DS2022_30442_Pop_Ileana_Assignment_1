using energy_utility_platform_api.Interfaces.ServiceInterfaces;
using energy_utility_platform_api.Middleware.Auth;
using energy_utility_platform_api.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace energy_utility_platform_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        public IActionResult Login([FromQuery] string name, [FromQuery] string password)
        {
            Console.WriteLine("start login");
            AuthenticateRequest request = new AuthenticateRequest();
            request.Name = name;
            request.Password = password;
            IActionResult response = Unauthorized();
            var token = _authService.Authenticate(request.Name,request.Password);

            if(token != null)
            {
                //HttpContext.Session.SetString("JWToken", token);
                response = Ok(token);
            }

            return response;
        }
    }
}
