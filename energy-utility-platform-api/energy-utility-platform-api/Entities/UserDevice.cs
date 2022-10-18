namespace energy_utility_platform_api.Entities
{
    public class UserDevice
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid EnergyDeviceId { get; set; }
        public string Address { get; set; }
        public User User { get; set; }
        public EnergyDevice EnergyDevice { get; set;}
        public List<EnergyConsumption> EnergyConsumptions { get; set; }

    }
}
