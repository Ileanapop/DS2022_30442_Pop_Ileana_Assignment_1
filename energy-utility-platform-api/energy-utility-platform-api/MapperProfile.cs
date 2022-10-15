using AutoMapper;
using energy_utility_platform_api.Dtos;
using energy_utility_platform_api.Entities;
using energy_utility_platform_api.ViewModels;

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
            CreateMap<User, UserViewModel>();
            
            #endregion
        }
    }
}
