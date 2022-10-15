namespace energy_utility_platform_api.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public UserTypeEnum Type { get; set; }
        public List<UserDevice> UserDevices { get; set; }

    }
}
