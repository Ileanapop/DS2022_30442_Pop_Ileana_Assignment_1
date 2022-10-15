using energy_utility_platform_api.Entities;
using energy_utility_platform_api.Entities.DataPersistence;
using energy_utility_platform_api.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace energy_utility_platform_api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UtilityPlatformContext _utilityPlatformContext;
        public UserRepository(UtilityPlatformContext utilityPlatformContext)
        {
            _utilityPlatformContext = utilityPlatformContext;
        }

        public async Task<User> Add(User user)
        {
            var newId = Guid.NewGuid();
            user.Id = newId;

            await _utilityPlatformContext.Users.AddAsync(user);

            await _utilityPlatformContext.SaveChangesAsync();

            return user;
        }


        public async Task<User> GetUserById(Guid id)
        {
            var result = await _utilityPlatformContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if(result == null)
            {
                return new User();
            }

            return result;

        }

        public async Task<User> GetUserByName(string name)
        {
            var existingUser = await _utilityPlatformContext.Users.FirstOrDefaultAsync(x => x.Name == name);

            if(existingUser == null)
            {
                return new User();
            }

            return existingUser;
        }

        public async Task<User> Update(User user)
        {
            var userToUpdate = await _utilityPlatformContext.Users
                .FirstOrDefaultAsync(x => x.Id == user.Id);

            if(userToUpdate is null)
            {
                return new User();
            }

            userToUpdate.Name = user.Name;
            userToUpdate.Password = user.Password;
            userToUpdate.Type = user.Type;

            await _utilityPlatformContext.SaveChangesAsync();

            return userToUpdate;

        }

        public async Task<User> Delete(Guid id)
        {
            var result = await _utilityPlatformContext.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if(result is null)
            {
                return new User();
            }

            _utilityPlatformContext.Users.Remove(result);

            await _utilityPlatformContext.SaveChangesAsync();

            return result;
        }
    }
}
