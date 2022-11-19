namespace energy_utility_platform_api.MessageConsumer
{
    public class ReadingDTO
    {
        public DateTime Timestamp { get; set; }
        public Guid DeviceId { get; set; }

        public float MeasurementValue { get; set; }
    }
}
