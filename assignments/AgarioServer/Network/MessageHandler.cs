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
                    var playerPositionMessage = JsonSerializer.Deserialize<Vector2Message>(inputJson, options);
                    playerClient.playerState.IllegalMovement= MovementLegality.EvaluateMovement(playerPositionMessage, playerClient);
                    playerClient.playerState.xPos = Math.Clamp(playerPositionMessage.x, -50, 50);
                    playerClient.playerState.yPos = Math.Clamp(playerPositionMessage.y, -50, 50);
                    Console.WriteLine($"{playerClient.playerState.playerName} position: X={playerClient.playerState.xPos},Y={playerClient.playerState.yPos}");
                    break;

                default:
                    throw new Exception("ERROR: Specific message not found on server!");
            }
           
        }
    }
}