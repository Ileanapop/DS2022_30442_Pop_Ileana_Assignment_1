using AutoMapper;
using energy_utility_platform_api.Dtos;
using energy_utility_platform_api.Entities;
using energy_utility_platform_api.Interfaces.RepositoryInterfaces;
using energy_utility_platform_api.Interfaces.ServiceInterfaces;
using energy_utility_platform_api.Middleware.Auth;
using energy_utility_platform_api.Utils.CustomExceptions;
using energy_utility_platform_api.ViewModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace energy_utility_platform_api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        //private readonly AuthorizationSettings _authorizationSettings;
        //private readonly byte[] _salt;

        public UserService(IUserRepository userRepository, IMapper mapper, IOptions<AuthorizationSettings> appSettings)
        {
           /* _authorizationSettings = appSettings.Value;
            _salt = new byte[128 / 8];
            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(_salt);
            }*/


            _userRepository = userRepository;
            _mapper = mapper;
        }

        /*public AuthorizationSettings GetAuthSettings()
        {
            return _authorizationSettings;
        }

        public string Authenticate(string username, string password)
        {
            var user =  _userRepository.GetUserByNameNonAsync(username);

            if (user is null)
                return null;
            if (user.Password == PasswordHasher.HashPassword(password))
            {
                return GenerateJwtToken(user);
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
        }*/

        public async Task<User> AddUser(UserForCreateDto user)
        {
            var newUser = _mapper.Map<User>(user);

            var existingUserWithSameName = await _userRepository.GetUserByName(newUser.Name);

            if (existingUserWithSameName.Id != Guid.Empty)
            {
                throw new ConflictException("User already exists");
            }

            var result = await _userRepository.Add(newUser);
            return result;
        }


        public async Task<User> GetUser(Guid id)
        {
            var result = await _userRepository.GetUserById(id);

            return result;
        }

        public async Task<User> GetUserByName(string name)
        {
            var result = await _userRepository.GetUserByName(name);

            return result;
        }

        public async Task<User> Update(UserForUpdateDto user)
        {
            var existingUserById = await _userRepository.GetUserById(user.Id);

            if(existingUserById.Id == Guid.Empty)
            {
                throw new NotFoundException("User not found");
            }

            var existingUserByName = await _userRepository.GetUserByName(user.Name);

            if(existingUserByName.Id != Guid.Empty && existingUserByName.Id != user.Id)
            {
                throw new ConflictException("Name already exists");
            }

            //var definedUserType = Enum.IsDefined(typeof(UserTypeEnum), user.Type);

            var result = await _userRepository.Update(_mapper.Map<User>(user));

            return result;

        }

        public async Task<User> Delete(Guid id)
        {
            var result = await _userRepository.Delete(id);

            return result;
        }

        public async Task<List<User>> GetAll()
        {
            var result = await _userRepository.GetAllUsers();

            return result;
        }
    }
}
