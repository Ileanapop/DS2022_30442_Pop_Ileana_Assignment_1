using AutoMapper;
using energy_utility_platform_api.Dtos;
using energy_utility_platform_api.Entities;
using energy_utility_platform_api.Interfaces.RepositoryInterfaces;
using energy_utility_platform_api.Interfaces.ServiceInterfaces;
using energy_utility_platform_api.Utils.CustomExceptions;

namespace energy_utility_platform_api.Services
{
    public class EnergyConsumptionService : IEnergyConsumptionService
    {

        private readonly IUserDeviceRepository _userDeviceRepository;
        private readonly IEnergyConsumptionRepository _energyConsumptionRepository;
        private readonly IMapper _mapper;

        public EnergyConsumptionService(IUserDeviceRepository userDeviceRepository, IEnergyConsumptionRepository energyConsumptionRepository, IMapper mapper)
        {
            _userDeviceRepository = userDeviceRepository;
            _energyConsumptionRepository = energyConsumptionRepository;
            _mapper = mapper;
        }

        public async Task<EnergyConsumption> Add(EnergyConsumptionDtoForCreate energyConsumptionDto)
        {
            var energyConsumption = _mapper.Map<EnergyConsumption>(energyConsumptionDto);

            var userDevice = await _userDeviceRepository.GetUserDeviceById(energyConsumptionDto.UserDeviceId);

            if (userDevice.Id == Guid.Empty)
            {
                throw new NotFoundException("User Device not mapped");
            }

            energyConsumption.UserDevice = userDevice;

            var result = await _energyConsumptionRepository.Add(energyConsumption);

            return result;
        }

    }
}
