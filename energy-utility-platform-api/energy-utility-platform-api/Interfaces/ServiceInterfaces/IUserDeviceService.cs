using energy_utility_platform_api.Dtos;
using energy_utility_platform_api.Entities;

namespace energy_utility_platform_api.Interfaces.ServiceInterfaces
{
    public interface IUserDeviceService
    {
        Task<UserDevice> Add(UserDeviceForCreateDto userDeviceDto);
    }
}
