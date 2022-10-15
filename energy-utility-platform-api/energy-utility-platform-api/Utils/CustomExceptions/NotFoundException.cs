namespace energy_utility_platform_api.Utils.CustomExceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() { }

        public NotFoundException(string message)
            : base(message)
        {

        }
    }
}
