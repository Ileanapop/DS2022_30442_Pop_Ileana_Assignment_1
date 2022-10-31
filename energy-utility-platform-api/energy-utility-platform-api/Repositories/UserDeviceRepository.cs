using energy_utility_platform_api.Entities;
using energy_utility_platform_api.Entities.DataPersistence;
using energy_utility_platform_api.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace energy_utility_platform_api.Repositories
{
    public class UserDeviceRepository : IUserDeviceRepository
    {
        private readonly UtilityPlatformContext _utilityPlatformContext;
        public UserDeviceRepository(UtilityPlatformContext utilityPlatformContext)
        {
            _utilityPlatformContext = utilityPlatformContext;
        }

        public async Task<UserDevice> Add(UserDevice userDevice)
        {
            var newId = Guid.NewGuid();

            userDevice.Id = newId;

            await _utilityPlatformContext.UserDevices.AddAsync(userDevice);

            await _utilityPlatformContext.SaveChangesAsync();

            return userDevice;
        }

        public async Task<UserDevice> GetUserDeviceById(Guid id)
        {
            var result = await _utilityPlatformContext.UserDevices.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return new UserDevice();
            }

            return result;
        }

    }
}
