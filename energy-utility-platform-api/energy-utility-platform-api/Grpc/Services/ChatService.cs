using energy_utility_platform_api.Grpc.Protos;
using energy_utility_platform_api.MessageConsumer;
using Grpc.Core;

namespace energy_utility_platform_api.Grpc.Services
{
    public class ChatService : energy_utility_platform_api.Grpc.Protos.ChatService.ChatServiceBase
    {
        private static List<User> usersInChat = new();
        private static Dictionary<string,IServerStreamWriter<ChatMessage>> observers = new();

        public override async Task<JoinResponse> JoinChat(User request, ServerCallContext context)
        {

            foreach(var u in usersInChat){
                Console.WriteLine(u.Id);
            }

            foreach(var o in observers){
                Console.WriteLine(o.Key);
                Console.WriteLine(o.Value);
            }

            if (usersInChat.Contains(request)){
                return new JoinResponse { Error = 1, Msg = "You are already in chat" };
            }

            Console.WriteLine(request);

            if (request.Type != "admin")
            {
                Console.WriteLine("It is client");

                foreach (var user in usersInChat)
                {
                    Console.WriteLine(user.Type);
                    if (user.Type == "admin")
                    {
                        usersInChat.Add(request);

                        if (observers.ContainsKey(user.Id))
                        {
                            var admin_stream = observers[user.Id];
                            ChatMessage joinMessage = new() { From = request.Id, To = user.Id, Msg = "Client " + request.Name + " joined chat" };

                            await admin_stream.WriteAsync(joinMessage);
                            return new JoinResponse { Error = 0, Msg = user.Id };
                        }
                        else
                        {
                            return new JoinResponse { Error = 1, Msg = "Error when trying to connect to admin" };
                        }
                    }
                }

                return new JoinResponse { Error = 1, Msg = "No admins available, try again" };
            }

            usersInChat.Add(request);
            return new JoinResponse { Error = 0, Msg = "Joined chat as admin" };
        }

        public override async Task ReceiveMsg(ReceiveMsgRequest request, IServerStreamWriter<ChatMessage> responseStream, ServerCallContext context)
        {

            if (observers.ContainsKey(request.Id))
            {
                observers.Remove(request.Id);
            }
            observers.Add(request.Id, responseStream);
            foreach(var o in observers){
                Console.WriteLine(o.Key);
                Console.WriteLine(o.Value);
            }
            //ChatMessage chatMessage = new() {From = "1", To = "1",Msg = "hei"};
            //await responseStream.WriteAsync(chatMessage);
            while (!context.CancellationToken.IsCancellationRequested)
            {
                await responseStream.WriteAsync(new ChatMessage{From = ""});
                await Task.Delay(TimeSpan.FromSeconds(1), context.CancellationToken);
            }
        }


        public override Task<Empty> SendMsg(ChatMessage request, ServerCallContext context)
        {
            foreach (var observer in observers)
            {
                if(observer.Key == request.To)
                {
                    observer.Value.WriteAsync(request);
                }
            }

            return Task.FromResult(new Empty());
        }
    }
}
