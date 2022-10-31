using energy_utility_platform_api.Entities.DataPersistence;
using energy_utility_platform_api.Entities;
using energy_utility_platform_api.Interfaces.RepositoryInterfaces;

namespace energy_utility_platform_api.Repositories
{
    public class EnergyConsumptionRepository :IEnergyConsumptionRepository
    {

        private readonly UtilityPlatformContext _utilityPlatformContext;
        public EnergyConsumptionRepository(UtilityPlatformContext utilityPlatformContext)
        {
            _utilityPlatformContext = utilityPlatformContext;
        }

        public async Task<EnergyConsumption> Add(EnergyConsumption energyConsumption)
        {
            var newId = Guid.NewGuid();
            energyConsumption.Id = newId;

            await _utilityPlatformContext.EnergyConsumptions.AddAsync(energyConsumption);

            await _utilityPlatformContext.SaveChangesAsync();

            return energyConsumption;
        }
    }
}
