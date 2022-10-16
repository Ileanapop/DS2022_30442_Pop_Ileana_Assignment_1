using energy_utility_platform_api.Dtos;
using energy_utility_platform_api.Entities;

namespace energy_utility_platform_api.Interfaces.ServiceInterfaces
{
    public interface IEnergyDeviceService
    {
        public Task<EnergyDevice> Add(EnergyDeviceForCreateDto energyDeviceForCreateDto);
        public Task<EnergyDevice> GetById(Guid id);
        public Task<EnergyDevice> GetByModelName(string name);

        public Task<EnergyDevice> Update(EnergyDeviceForUpdateDto user);

        public Task<EnergyDevice> Delete(Guid id);
    }
}
