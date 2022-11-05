using energy_utility_platform_api.Entities;

namespace energy_utility_platform_api.Interfaces.RepositoryInterfaces
{
    public interface IEnergyConsumptionRepository
    {
        public Task<EnergyConsumption> Add(EnergyConsumption energyConsumption);
        public Task<List<EnergyConsumption>> Get(Guid id);
        public Task<List<EnergyConsumption>> GetEnrgyConsumptionByDay(Guid id, DateTime date);
    }
}
