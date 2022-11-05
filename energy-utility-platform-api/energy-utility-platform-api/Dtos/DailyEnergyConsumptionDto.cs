using System.ComponentModel.DataAnnotations;

namespace energy_utility_platform_api.Dtos
{
    public class DailyEnergyConsumptionDto
    {
        [Required]
        public Guid UserDeviceId { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
    }
}
