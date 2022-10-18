using energy_utility_platform_api.Entities;

namespace energy_utility_platform_api.ViewModels
{
    public class UserDeviceViewModel
    {    
        public Guid Id { get; set; }
        public string Address { get; set; }
        public UserViewModelWithoutList User { get; set; }
        public EnergyDeviceViewModelWithoutList EnergyDevice { get; set; }
        public List<EnergyConsumptionViewModel> EnergyConsumptions { get; set; }
    }
}
