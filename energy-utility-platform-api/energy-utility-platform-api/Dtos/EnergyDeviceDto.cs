namespace energy_utility_platform_api.Dtos
{
    public class EnergyDeviceDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string MaxHourlyEnergy { get; set; }
    }
}
