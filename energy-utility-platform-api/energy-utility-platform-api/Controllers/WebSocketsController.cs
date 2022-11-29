using energy_utility_platform_api.Entities;
using energy_utility_platform_api.MessageConsumer;
using energy_utility_platform_api.Middleware.Auth;
using energy_utility_platform_api.PingServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace energy_utility_platform_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebSocketsController : ControllerBase
    {

        private ConnectedClientsRepository _connectedClientsRepository;

        public WebSocketsController()
        {
            _connectedClientsRepository = ConnectedClientsRepository.GetInstance();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("CLIENT connected");
                Console.WriteLine("--------------------------------------------");

                //receive userId from client
                var buffer = new byte[1024 * 4];
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                var userId = Encoding.UTF8.GetString(buffer,0,result.Count);

                //create connected client
                ConnectedClient connectedClient = new ConnectedClient();
                connectedClient.ClientSocket = webSocket;
                Console.WriteLine("!"+userId+"!");
                connectedClient.ClientId = userId;
                _connectedClientsRepository.AddClient(connectedClient);

               
                /*string warning = "you are connected";
                var serverMsg = Encoding.UTF8.GetBytes(warning);
                await connectedClient.ClientSocket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), 0, true, CancellationToken.None);*/

                while (!result.CloseStatus.HasValue)
                {
                    await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);
                    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                }

                /*PingServerService serverService = new PingServerService(connectedClient);

                Thread thr = new Thread(new ThreadStart(serverService.ExecuteContinuousPings));
                thr.Start();*/

            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }

            
        }
        
    }
}
