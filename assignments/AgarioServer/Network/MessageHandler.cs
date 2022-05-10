using System.Text.Json;
using AgarioServer.Messages;
using AgarioServer.Model;

namespace AgarioServer.Network;

public class MessageHandler
{
    static JsonSerializerOptions options = new ()
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
        var streamReader = new StreamReader(playerClient.PlayerTcpClient.GetStream());
       
        while (true)
        {
            var inputJson = await streamReader.ReadLineAsync();
           
            var message = JsonSerializer.Deserialize<Message>(inputJson, options);
            
            switch (message.MessageName)
            {
                case MessagesEnum.LogInMessage:
                    var logInMessage = JsonSerializer.Deserialize<LogInMessage>(inputJson, options);
                    Console.WriteLine($"{logInMessage.PlayerName} ({playerClient.PlayerTcpClient.Client.RemoteEndPoint}) joined the server!");
                    playerClient.PlayerState.PlayerName = logInMessage.PlayerName;
 
                    break;
                case MessagesEnum.ServerIdAssignmentMessage:
                    break;
                
                case MessagesEnum.StringMessage:
                    var stringMessage = JsonSerializer.Deserialize<StringMessage>(inputJson, options);
                    Console.WriteLine(stringMessage.StringText);
                    break;

                case MessagesEnum.Vector2Message:
                    var playerPositionMessage = JsonSerializer.Deserialize<Vector2Message>(inputJson, options);
                    
                    playerClient.PlayerState.IllegalMovement= MovementLegality.EvaluateMovement(playerPositionMessage, playerClient);
                    
                    playerClient.PlayerState.XPos = Math.Clamp(playerPositionMessage.X, -GameState.BoardSizeX/2, GameState.BoardSizeX/2);
                    playerClient.PlayerState.YPos = Math.Clamp(playerPositionMessage.Y, -GameState.BoardSizeY/2, GameState.BoardSizeY/2);
                    // Console.WriteLine($"{playerClient.PlayerState.PlayerName} position: X={playerClient.PlayerState.XPos},Y={playerClient.PlayerState.YPos}");
                    break;

                case MessagesEnum.BoolMessage:
                    break;
                case MessagesEnum.SpawnOrbMessage:
                    break;
                default:
                    throw new Exception("ERROR: Specific message not found on server!");
            }
           
        }
    }
}