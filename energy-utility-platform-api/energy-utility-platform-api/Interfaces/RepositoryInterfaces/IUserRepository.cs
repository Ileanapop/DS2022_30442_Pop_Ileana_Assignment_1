using energy_utility_platform_api.Dtos;
using energy_utility_platform_api.Entities;

namespace energy_utility_platform_api.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository
    {
        public Task<User> Add(User user);

        public Task<User> GetUserByName(string name);
        public Task<User> GetUserById(Guid id);

        public Task<User> Update(User user);
        public Task<User> Delete(Guid id);

        public User GetUserByNameNonAsync(string name);

        public Task<List<User>> GetAllUsers();
    }
}
