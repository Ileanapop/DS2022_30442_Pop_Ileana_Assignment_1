using energy_utility_platform_api.Entities;
using energy_utility_platform_api.Entities.DataPersistence;
using energy_utility_platform_api.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace energy_utility_platform_api.Repositories
{
    public class EnergyDeviceRepository : IEnergyDeviceRepository
    {
        private readonly UtilityPlatformContext _utilityPlatformContext;
        public EnergyDeviceRepository(UtilityPlatformContext utilityPlatformContext)
        {
            _utilityPlatformContext = utilityPlatformContext;
        }

        public async Task<EnergyDevice> Add(EnergyDevice energyDevice)
        {
            var newId = Guid.NewGuid();
            energyDevice.Id = newId;

            await _utilityPlatformContext.EnergyDevices.AddAsync(energyDevice);

            await _utilityPlatformContext.SaveChangesAsync();

            return energyDevice;
        }

        public async Task<EnergyDevice> Delete(Guid id)
        {
            var result = await _utilityPlatformContext.EnergyDevices
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result is null)
            {
                return new EnergyDevice();
            }

            _utilityPlatformContext.EnergyDevices.Remove(result);

            await _utilityPlatformContext.SaveChangesAsync();

            return result;
        }

        public async Task<EnergyDevice> GetEnergyDeviceById(Guid id)
        {
            var result = await _utilityPlatformContext.EnergyDevices.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return new EnergyDevice();
            }

            return result;
        }

        public async Task<EnergyDevice> GetEnergyDeviceByModelName(string name)
        {
            var existingEnergyDevice = await _utilityPlatformContext.EnergyDevices.FirstOrDefaultAsync(x => x.ModelName == name);

            if (existingEnergyDevice == null)
            {
                return new EnergyDevice();
            }

            return existingEnergyDevice;
        }

        public async Task<EnergyDevice> Update(EnergyDevice energyDevice)
        {
            var energyDeviceToUpdate = await _utilityPlatformContext.EnergyDevices
                .FirstOrDefaultAsync(x => x.Id == energyDevice.Id);

            if (energyDeviceToUpdate is null)
            {
                return new EnergyDevice();
            }

            energyDeviceToUpdate.ModelName = energyDevice.ModelName;
            energyDeviceToUpdate.Description = energyDevice.Description;
            energyDeviceToUpdate.MaxHourlyEnergy = energyDevice.MaxHourlyEnergy;

            await _utilityPlatformContext.SaveChangesAsync();

            return energyDeviceToUpdate;
        }
    }
}
