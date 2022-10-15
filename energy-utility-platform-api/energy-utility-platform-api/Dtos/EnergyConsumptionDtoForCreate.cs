using System.ComponentModel.DataAnnotations;

namespace energy_utility_platform_api.Dtos
{
    public class EnergyConsumptionDtoForCreate
    {
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public float Consumption { get; set; }
        [Required]
        public Guid UserDeviceId { get; set; }

    }
}
