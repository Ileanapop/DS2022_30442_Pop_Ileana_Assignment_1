using System.ComponentModel.DataAnnotations;

namespace energy_utility_platform_api.Dtos
{
    public class EnergyDeviceForCreateDto
    {
        [Required]
        public string ModelName { get; set; }
        [Required]
        public string Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        [Required]
        public float MaxHourlyEnergy { get; set; }
    }
}
