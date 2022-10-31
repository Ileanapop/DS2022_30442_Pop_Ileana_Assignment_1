using energy_utility_platform_api.Dtos;
using energy_utility_platform_api.Entities;

namespace energy_utility_platform_api.Interfaces.ServiceInterfaces
{
    public interface IEnergyConsumptionService
    {
        public Task<EnergyConsumption> Add(EnergyConsumptionDtoForCreate energyConsumptionForCreateDto);
    }
}
