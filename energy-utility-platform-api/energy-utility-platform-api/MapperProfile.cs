using AutoMapper;
using energy_utility_platform_api.Dtos;
using energy_utility_platform_api.Entities;
using energy_utility_platform_api.ViewModels;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace energy_utility_platform_api
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region User Profiles
            CreateMap<UserForCreateDto, User>()
                .AfterMap
                (
                    (src, dst) =>
                    {
                        dst.Type = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), src.Type);
                    }
                );
            CreateMap<UserForUpdateDto, User>()
                .AfterMap
                (
                    (src, dst) =>
                    {
                        dst.Type = (UserTypeEnum)Enum.Parse(typeof(UserTypeEnum), src.Type);
                    }
                );
            CreateMap<User, UserViewModel>()
                .ForMember(
                    dst => dst.Devices,
                    opt => opt.MapFrom(src => src.UserDevices)
                );

            #endregion

            #region Energy device profiles

            CreateMap<EnergyDeviceForCreateDto, EnergyDevice>();
            CreateMap<EnergyDeviceForUpdateDto, EnergyDevice>();

            CreateMap<EnergyDevice, EnergyDeviceViewModel>();

            #endregion

            #region User - Device Profiles
            CreateMap<UserDeviceForCreateDto, UserDevice>();

            CreateMap<User, UserViewModelWithoutList>();
            CreateMap<EnergyDevice, EnergyDeviceViewModelWithoutList>();

            CreateMap<UserDevice, UserDeviceViewModel>();
            //.ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User);

            #endregion

            #region EnergyConsumption profiles

            CreateMap<EnergyConsumptionDtoForCreate, EnergyConsumption>();
            CreateMap<EnergyConsumption, EnergyConsumptionViewModel>();

            CreateMap<EnergyConsumption, DailyEnergyConsumptionViewModel>()
                .ForMember(
                    dst => dst.Hour,
                    opt => opt.MapFrom(src => src.DateTime.Hour)
                )
                .ForMember(
                    dst => dst.Consumption,
                    opt => opt.MapFrom(src => src.Consumption)
                );


            #endregion
        }
    }
}
