using energy_utility_platform_api.Grpc.Protos;
using Grpc.Core;

namespace energy_utility_platform_api.Grpc.Services
{
    public class ChatService : energy_utility_platform_api.Grpc.Protos.ChatService.ChatServiceBase
    {
        public override Task<JoinResponse> JoinChat(User request, ServerCallContext context)
        {
            Console.WriteLine("here in join chat");
            Console.WriteLine(request.Id);
            return Task.FromResult(new JoinResponse { Error = 0, Msg = "Hello" });
        }
    }
}
