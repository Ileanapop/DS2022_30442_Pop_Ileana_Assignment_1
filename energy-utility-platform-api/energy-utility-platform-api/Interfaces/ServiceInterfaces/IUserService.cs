using energy_utility_platform_api.Dtos;
using energy_utility_platform_api.Entities;

namespace energy_utility_platform_api.Interfaces.ServiceInterfaces
{
    public interface IUserService
    {
        public Task<User> AddUser(UserForCreateDto user);
        public Task<User> GetUser(Guid id);
        public Task<User> GetUserByName(string name);

        public Task<User> Update(UserForUpdateDto user);

        public Task<User> Delete(Guid id);
    }
}
