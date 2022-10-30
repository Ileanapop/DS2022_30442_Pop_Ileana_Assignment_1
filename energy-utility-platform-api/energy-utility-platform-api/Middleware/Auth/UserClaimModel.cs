namespace energy_utility_platform_api.Middleware.Auth
{
    public class UserClaimModel
    {
        public string Claim_User { get; set; }

        public IEnumerable<string> Claim_Roles { get; set; }
    }
}
