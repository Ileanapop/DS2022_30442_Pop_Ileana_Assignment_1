using System.ComponentModel.DataAnnotations;

namespace energy_utility_platform_api.Dtos
{
    public class UserForCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
