using AutoMapper;
using energy_utility_platform_api.Dtos;
using energy_utility_platform_api.Entities;
using energy_utility_platform_api.Interfaces.RepositoryInterfaces;
using energy_utility_platform_api.Interfaces.ServiceInterfaces;
using energy_utility_platform_api.Utils.CustomExceptions;

namespace energy_utility_platform_api.Services
{
    public class UserDeviceService : IUserDeviceService
    {

        private readonly IUserDeviceRepository _userDeviceRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEnergyDeviceRepository _energyDeviceRepository;
        private readonly IMapper _mapper;

        public UserDeviceService(IUserDeviceRepository userDeviceRepository, IUserRepository userRepository, IEnergyDeviceRepository energyDeviceRepository, IMapper mapper)
        {
            _userDeviceRepository = userDeviceRepository;
            _userRepository = userRepository;
            _energyDeviceRepository = energyDeviceRepository;
            _mapper = mapper;
        }

        public async Task<UserDevice> Add(UserDeviceForCreateDto userDeviceDto)
        {
            var userDevice = _mapper.Map<UserDevice>(userDeviceDto);

            var user = await _userRepository.GetUserById(userDeviceDto.UserId);

            Console.WriteLine(user);

            if(user.Id == Guid.Empty)
            {
                throw new NotFoundException("User not registered");
            }

            if(user.Type != UserTypeEnum.client)
            {
                throw new NotFoundException("User not registered");
            }

            userDevice.User = user;

            var energyDevice = await _energyDeviceRepository.GetEnergyDeviceById(userDeviceDto.EnergyDeviceId);

            if(energyDevice.Id == Guid.Empty)
            {
                throw new NotFoundException("Energy device not registered");
            }

            userDevice.EnergyDevice = energyDevice;

            var result = await _userDeviceRepository.Add(userDevice);

            return result;
        }
    }
}
