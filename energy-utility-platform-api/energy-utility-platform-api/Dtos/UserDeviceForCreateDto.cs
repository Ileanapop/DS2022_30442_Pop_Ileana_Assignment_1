using System.ComponentModel.DataAnnotations;

namespace energy_utility_platform_api.Dtos
{
    public class UserDeviceForCreateDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid EnergyDeviceId { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
