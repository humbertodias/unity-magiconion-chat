using Chat.Shared;
using Grpc.Core;
using Grpc.Net.Client;
using MagicOnion.Client;

namespace Chat.Client;

class Program
{
    static async Task Main(string[] args)
    {
        // Connect to the server using gRPC channel.
        var channel = GrpcChannel.ForAddress("http://localhost:5000",new GrpcChannelOptions
        {
            Credentials = ChannelCredentials.Insecure
        });
        // Create a proxy to call the server transparently.
        var client = MagicOnionClient.Create<IChatService>(channel);

        // Chamadas para interagir com o serviço de chat
        Console.WriteLine("Digite seu nome de usuário:");
        var userName = Console.ReadLine();

        while (true)
        {
            Console.WriteLine("Digite uma mensagem (ou 'exit' para sair):");
            var message = Console.ReadLine();

            if (message?.ToLower() == "exit")
            {
                break;
            }
            
            // Envia a mensagem para o servidor de chat
            // Call the server-side method using the proxy.
            await client.SendReportAsync( new ChatMessage {UserName = userName, Message = message});
        }

        // Fecha a conexão com o servidor MagicOnion
        await channel.ShutdownAsync();
    }
}
