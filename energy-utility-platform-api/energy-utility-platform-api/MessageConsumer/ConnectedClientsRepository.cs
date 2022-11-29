using System.Net.WebSockets;

namespace energy_utility_platform_api.MessageConsumer
{
    public class ConnectedClientsRepository 
    {

        private Dictionary<string, WebSocket> connectedClients;
        private static ConnectedClientsRepository instance;

        public static ConnectedClientsRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new ConnectedClientsRepository();
            }
            return instance;
        }

        private ConnectedClientsRepository()
        {
            connectedClients = new Dictionary<string, WebSocket>();
        }

        public Dictionary<string,WebSocket> GetClients()
        {
            return connectedClients;
        }

        public void AddClient(ConnectedClient client)
        {
            if (connectedClients.ContainsKey(client.ClientId))
            {
                connectedClients.Remove(client.ClientId);
            }
            connectedClients.Add(client.ClientId, client.ClientSocket);
        }

        public WebSocket GetClientSocket(string clientId)
        {

            Console.WriteLine("!" + clientId+"!");
            Console.WriteLine(connectedClients);
            Console.WriteLine("------------------");
            Console.WriteLine(connectedClients.Count);
            foreach(var i in connectedClients.Keys)
            {
                Console.WriteLine("!"+i+"!");
                Console.WriteLine(String.Equals(clientId, i));
            }
            Console.WriteLine("------------------");
            

            if(connectedClients.ContainsKey(clientId))
            {
                Console.WriteLine("foundd");
                return connectedClients.GetValueOrDefault(clientId);
            }

            return null;
        }
    }
}
