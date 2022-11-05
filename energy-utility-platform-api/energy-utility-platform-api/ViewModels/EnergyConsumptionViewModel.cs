using energy_utility_platform_api.Entities;

namespace energy_utility_platform_api.ViewModels
{
    public class EnergyConsumptionViewModel
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public float Consumption { get; set; }
        public UserDeviceViewModel UserDevice { get; set; }
    }
}
