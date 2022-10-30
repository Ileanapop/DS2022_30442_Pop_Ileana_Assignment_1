using energy_utility_platform_api.Interfaces.ServiceInterfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace energy_utility_platform_api.Middleware.Auth
{
    public class Authentication
    {
        private readonly RequestDelegate _next;
        private readonly AuthorizationSettings _authorizationSettings;

        public Authentication(RequestDelegate next, IOptions<AuthorizationSettings> appSettings)
        {
            _next = next;
            _authorizationSettings = appSettings.Value;
        }

        public Task Invoke(HttpContext httpContext)
        {
            
            try
            {             
                string authHeader = httpContext.Request.Headers["Authorization"];
                string[] header = new string[3];

                if (authHeader != null)
                {
                    header = authHeader.Split(' ');
                }
                Console.WriteLine(header[1]);    
                //var token = httpContext.Session.GetString("JWToken");

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_authorizationSettings.Secret);

                Console.WriteLine(key);

                tokenHandler.ValidateToken(header[1], new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false
                }, out SecurityToken validatedToken); ;

                Console.WriteLine(validatedToken);

                //var jwtToken = (JwtSecurityToken)validatedToken;
                //var name = jwtToken.Claims.First(x => x.Type == "sub").Value;
                //httpContext.Items["Account"] = _authService.GetUserByName(name);

            }
            catch(Exception ex)
            {
                Console.WriteLine("exception");
            }
            Console.WriteLine("next");
            Console.WriteLine(httpContext.Request.Path);
            return _next(httpContext);
        }
    }

    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Authentication>();
        }
    }
}
