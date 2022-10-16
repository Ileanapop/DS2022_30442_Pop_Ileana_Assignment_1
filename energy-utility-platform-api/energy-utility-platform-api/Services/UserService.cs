using AutoMapper;
using energy_utility_platform_api.Dtos;
using energy_utility_platform_api.Entities;
using energy_utility_platform_api.Interfaces.RepositoryInterfaces;
using energy_utility_platform_api.Interfaces.ServiceInterfaces;
using energy_utility_platform_api.Utils.CustomExceptions;
using energy_utility_platform_api.ViewModels;

namespace energy_utility_platform_api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

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
    }
}
