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
    
    public static async Task ReadMessage(PlayerClient playerClient)
    {
        var streamReader = new StreamReader(playerClient.playerTcpClient.GetStream());
       
        while (true)
        {
            var inputJson = await streamReader.ReadLineAsync();
           
            var message = JsonSerializer.Deserialize<Message>(inputJson, options);
            
            switch (message.messageName)
            {
                case MessagesEnum.LogInMessage:
                    var logInMessage = JsonSerializer.Deserialize<LogInMessage>(inputJson, options);
                    Console.WriteLine($"{logInMessage.playerName} ({playerClient.playerTcpClient.Client.RemoteEndPoint}) joined the server!");
                    playerClient.playerState.playerName = logInMessage.playerName;
 
                    break;
                case MessagesEnum.ServerIdAssignmentMessage:
                    break;
                
                case MessagesEnum.StringMessage:
                    var stringMessage = JsonSerializer.Deserialize<StringMessage>(inputJson, options);
                    Console.WriteLine(stringMessage.stringText);
                    break;

                case MessagesEnum.Vector2Message:
                    var Vector2Message = JsonSerializer.Deserialize<Vector2Message>(inputJson, options);
                    
                    playerClient.playerState.xPos = Vector2Message.x;
                    playerClient.playerState.yPos = Vector2Message.y;
                    // Console.WriteLine($"{playerClient.playerState.playerName} position: X={Vector2Message.x},Y={Vector2Message.y}");
                    break;

                default:
                    throw new Exception("ERROR: Specific message not found on server!");
            }
           
        }
    }
}