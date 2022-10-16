using AutoMapper;
using energy_utility_platform_api.Dtos;
using energy_utility_platform_api.Entities;
using energy_utility_platform_api.Interfaces.RepositoryInterfaces;
using energy_utility_platform_api.Interfaces.ServiceInterfaces;
using energy_utility_platform_api.Repositories;
using energy_utility_platform_api.Utils.CustomExceptions;

namespace energy_utility_platform_api.Services
{
    public class EnergyDeviceService : IEnergyDeviceService
    {
        private readonly IEnergyDeviceRepository _energyDeviceRepository;
        private readonly IMapper _mapper;

        public EnergyDeviceService(IEnergyDeviceRepository energyDeviceRepository, IMapper mapper)
        {
            _energyDeviceRepository = energyDeviceRepository;
            _mapper = mapper;
        }

        public async Task<EnergyDevice> Add(EnergyDeviceForCreateDto energyDeviceForCreateDto)
        {
            var newEnergyDevice = _mapper.Map<EnergyDevice>(energyDeviceForCreateDto);

            var existingEnergyDeviceWithSameName = await _energyDeviceRepository
                .GetEnergyDeviceByModelName(energyDeviceForCreateDto.ModelName);

            if (existingEnergyDeviceWithSameName.Id != Guid.Empty)
            {
                throw new ConflictException("Device already registered");
            }

            var result = await _energyDeviceRepository.Add(newEnergyDevice);
            return result;
        }

        public async Task<EnergyDevice> GetById(Guid id)
        {
            var result = await _energyDeviceRepository.GetEnergyDeviceById(id);

            return result;
        }

        public async Task<EnergyDevice> GetByModelName(string name)
        {
            var result = await _energyDeviceRepository.GetEnergyDeviceByModelName(name);

            return result;
        }

        public async Task<EnergyDevice> Update(EnergyDeviceForUpdateDto energyDevice)
        {
            var existingEnergyDeviceById = await _energyDeviceRepository
                .GetEnergyDeviceById(energyDevice.Id);

            if (existingEnergyDeviceById.Id == Guid.Empty)
            {
                throw new NotFoundException("Energy device not registered");
            }

            var existingEnergyDeviceByModeName = await _energyDeviceRepository
                .GetEnergyDeviceByModelName(energyDevice.ModelName);

            if (existingEnergyDeviceByModeName.Id != Guid.Empty && existingEnergyDeviceByModeName.Id != energyDevice.Id)
            {
                throw new ConflictException("Energy device already registered");
            }

            var result = await _energyDeviceRepository.Update(_mapper.Map<EnergyDevice>(energyDevice));

            return result;

        }
        public async Task<EnergyDevice> Delete(Guid id)
        {
            var result = await _energyDeviceRepository.Delete(id);

            return result;
        }
    }
}
