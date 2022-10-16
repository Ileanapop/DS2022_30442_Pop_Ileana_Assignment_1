using energy_utility_platform_api.Entities;

namespace energy_utility_platform_api.Interfaces.RepositoryInterfaces
{
    public interface IUserDeviceRepository
    {
        public Task<UserDevice> Add(UserDevice userDevice);
    }
}
