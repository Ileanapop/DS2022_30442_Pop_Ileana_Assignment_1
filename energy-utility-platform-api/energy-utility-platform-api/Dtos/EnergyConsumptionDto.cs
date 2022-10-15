using energy_utility_platform_api.Entities;

namespace energy_utility_platform_api.Dtos
{
    public class EnergyConsumptionDto
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public float Consumption { get; set; }
        public Guid UserDeviceId { get; set; }

    }
}
