using energy_utility_platform_api.Dtos;

namespace energy_utility_platform_api.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public List<EnergyDeviceViewModel> Devices { get; set; }
    }
}
