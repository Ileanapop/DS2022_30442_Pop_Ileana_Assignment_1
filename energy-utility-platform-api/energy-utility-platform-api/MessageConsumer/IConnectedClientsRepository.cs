using System.Net.WebSockets;

namespace energy_utility_platform_api.MessageConsumer
{
    public interface IConnectedClientsRepository
    {
        public void AddClient(ConnectedClient client);

        public WebSocket GetClientSocket(string clientId);
    }
}
