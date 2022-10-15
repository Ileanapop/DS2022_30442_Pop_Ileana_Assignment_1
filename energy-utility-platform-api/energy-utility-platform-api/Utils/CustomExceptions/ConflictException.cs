namespace energy_utility_platform_api.Utils.CustomExceptions
{
    public class ConflictException : Exception
    {
        public ConflictException() { }

        public ConflictException(string message)
            : base(message)
        {

        }
    }
}
