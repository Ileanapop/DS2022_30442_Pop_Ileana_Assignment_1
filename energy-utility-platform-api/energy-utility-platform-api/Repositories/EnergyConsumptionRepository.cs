using energy_utility_platform_api.Entities.DataPersistence;
using energy_utility_platform_api.Entities;
using energy_utility_platform_api.Interfaces.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<EnergyConsumption>> Get(Guid id)
        {
            var result = await _utilityPlatformContext.EnergyConsumptions
                .Where(x => x.Id == id)
                .ToListAsync();

           if(result is null)
           {
                return new List<EnergyConsumption>();
           }

            return result;
        }

        public async Task<List<EnergyConsumption>> GetEnrgyConsumptionByDay(Guid id, DateTime date)
        {
            var dates =  _utilityPlatformContext.EnergyConsumptions.ToList();


            var result = await _utilityPlatformContext.EnergyConsumptions
                .Where(x => x.UserDevice.Id == id && date.Date.DayOfYear == x.DateTime.Date.DayOfYear && date.Date.Year == x.DateTime.Date.Year)
                .OrderBy(x => x.DateTime.Hour)
                .ToListAsync();

            if (result is null)
            {
                return new List<EnergyConsumption>();
            }


            return result;
        }
    }
}
