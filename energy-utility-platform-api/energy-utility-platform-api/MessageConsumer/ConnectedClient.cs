using System.Net.WebSockets;

namespace energy_utility_platform_api.MessageConsumer
{
    public class ConnectedClient
    {

        public WebSocket ClientSocket { get; set; }
        public string ClientId { get; set; }

        private async Task SendNotification()
        {
            Console.WriteLine("Sending notification ...");
        }
    }
}
