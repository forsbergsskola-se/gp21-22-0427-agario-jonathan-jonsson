using System.Net.Sockets;
using System.Text.Json;

namespace AgarioServer;

public class MessageHandler
{
    static JsonSerializerOptions options = new JsonSerializerOptions()
    {
        IncludeFields = true
    };
    
    public static async Task SendMessageAsync<T>(T message, StreamWriter streamWriter)
    {
        await streamWriter.WriteLineAsync(JsonSerializer.Serialize(message, options));
        await streamWriter.FlushAsync();
    }
    
    public static async Task ReadMessage(TcpClient client, PlayerState playerState)
    {
        var streamReader = new StreamReader(client.GetStream());
       
        while (true)
        {
            var inputJson = await streamReader.ReadLineAsync();
           
            var message = JsonSerializer.Deserialize<Message>(inputJson, options);

            switch (message.messageName)
            {
                case MessagesEnum.LogInMessage:
                    var specificMessage = JsonSerializer.Deserialize<LogInMessage>(inputJson, options);
                    Console.WriteLine($"{specificMessage.playerName} ({client.Client.RemoteEndPoint}) joined the server!");
                    playerState.playerName = specificMessage.playerName;
 
                    break;
                case MessagesEnum.ServerIdAssignmentMessage:
                    break;
                case MessagesEnum.StringMessage:
                    break;

                case MessagesEnum.Vector2Message:
                    var Vector2Message = JsonSerializer.Deserialize<Vector2Message>(inputJson, options);
                    Console.WriteLine($"vector2message: X={Vector2Message.x},Y={Vector2Message.y}");
                    break;

                default:
                    throw new Exception("ERROR: Specific message not found on server!");
            }
           
        }
    }
}