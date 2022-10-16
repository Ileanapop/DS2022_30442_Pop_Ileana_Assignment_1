using energy_utility_platform_api.Entities;

namespace energy_utility_platform_api.ViewModels
{
    public class EnergyDeviceViewModel
    {
        public Guid Id { get; set; }
        public string ModelName { get; set; }
        public string Description { get; set; }
        public float MaxHourlyEnergy { get; set; }
        public List<UserDeviceViewModel> UserDevices { get; set; }
    }
}
