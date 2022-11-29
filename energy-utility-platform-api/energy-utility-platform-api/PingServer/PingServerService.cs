using energy_utility_platform_api.MessageConsumer;
using System.Text;

namespace energy_utility_platform_api.PingServer
{
    public class PingServerService 
    {

        private readonly ConnectedClient connectedClient;

        public PingServerService(ConnectedClient connectedClient)
        {
            this.connectedClient = connectedClient;
        }

        public void ExecuteContinuousPings()
        {
            while (true)     
            {            
                     
                string warning = "ping message";
                var serverMsg = Encoding.UTF8.GetBytes(warning);
                connectedClient.ClientSocket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), 0, true, CancellationToken.None);
                
            }
        }
    }
}
