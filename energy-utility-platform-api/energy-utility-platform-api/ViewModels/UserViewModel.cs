﻿using energy_utility_platform_api.Dtos;

namespace energy_utility_platform_api.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Password { get; set; }
        public List<UserDeviceViewModel> Devices { get; set; }
    }
}
