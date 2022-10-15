namespace energy_utility_platform_api.Entities
{
    public class EnergyConsumption
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public float Consumption { get; set; }
        public UserDevice UserDevice { get; set; }

    }
}
