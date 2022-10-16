using energy_utility_platform_api.Entities;
using energy_utility_platform_api.Entities.DataPersistence;
using energy_utility_platform_api.Interfaces.RepositoryInterfaces;

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

            await _utilityPlatformContext.UserDevices.AddAsync(userDevice);

            await _utilityPlatformContext.SaveChangesAsync();

            return userDevice;
        }

    }
}
