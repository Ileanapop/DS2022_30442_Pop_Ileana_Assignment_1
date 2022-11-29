using AutoMapper;
using energy_utility_platform_api.Entities;
using energy_utility_platform_api.Interfaces.RepositoryInterfaces;
using energy_utility_platform_api.Interfaces.ServiceInterfaces;
using energy_utility_platform_api.Middleware.Auth;
using energy_utility_platform_api.ViewModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace energy_utility_platform_api.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthorizationSettings _authorizationSettings;
        private readonly byte[] _salt;
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository, IOptions<AuthorizationSettings> appSettings)
        {
             _authorizationSettings = appSettings.Value;
             _salt = new byte[128 / 8];
             using(var rng = RandomNumberGenerator.Create())
             {
                 rng.GetBytes(_salt);
             }


            _userRepository = userRepository;
        }

        public AuthorizationSettings GetAuthSettings()
        {
            return _authorizationSettings;
        }

        public LoginViewModel Authenticate(string username, string password)
        {
            var user = _userRepository.GetUserByNameNonAsync(username);

            if (user is null)
                return null;

            if (user.Password == PasswordHasher.HashPassword(password))
            {
                LoginViewModel loginViewModel = new LoginViewModel();
                loginViewModel.Key = GenerateJwtToken(user);
                loginViewModel.UserRole = user.Type.ToString();
                loginViewModel.UserId = user.Id;
                return loginViewModel;
            }
            return null;
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authorizationSettings.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Name),
                new Claim("name", user.Name),
                new Claim("role", user.Type.ToString()),

            };

            var token = new JwtSecurityToken(
                issuer: _authorizationSettings.Issuer,
                audience: _authorizationSettings.Audience,
                claims: claims,
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
