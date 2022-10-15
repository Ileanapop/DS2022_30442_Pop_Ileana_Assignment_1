namespace energy_utility_platform_api.Entities
{
    public class EnergyDevice
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public float MaxHourlyEnergy { get; set; }
        public List<UserDevice> UserDevices { get; set; }
    }
}
