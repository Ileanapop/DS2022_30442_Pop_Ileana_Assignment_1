using energy_utility_platform_api.Entities;

namespace energy_utility_platform_api.Interfaces.RepositoryInterfaces
{
    public interface IEnergyDeviceRepository
    {
        public Task<EnergyDevice> Add(EnergyDevice energyDevice);     
        public Task<EnergyDevice> GetEnergyDeviceById(Guid id);
        public Task<EnergyDevice> GetEnergyDeviceByModelName(string name);
        public Task<EnergyDevice> Update(EnergyDevice energyDevice);
        public Task<EnergyDevice> Delete(Guid id);

        public Task<List<EnergyDevice>> GetAll();
    }
}
