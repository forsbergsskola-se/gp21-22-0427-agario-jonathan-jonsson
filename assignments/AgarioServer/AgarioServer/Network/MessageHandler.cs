using System.Numerics;
using System.Text.Json;
using AgarioServer.Model;
using Assets.Scripts.AgarioShared.Model;
using Assets.Scripts.AgarioShared.Network;
using Assets.Scripts.AgarioShared.Network.Messages;

namespace AgarioServer.Network;

public class MessageHandler
{
    static JsonSerializerOptions options = new ()
    {
        IncludeFields = true
    };
    
    public static async Task SendMessageAsync<T>(T message, StreamWriter streamWriter)
    {
        lock (streamWriter)
        {
         
            streamWriter.WriteLineAsync(JsonSerializer.Serialize(message, options));
            streamWriter.FlushAsync();

        }
    }
    
    public static async Task ReadMessage(PlayerClient playerClient)
    {
        var streamReader = new StreamReader(playerClient.PlayerTcpClient.GetStream());
       
        while (true)
        {
            var inputJson = await streamReader.ReadLineAsync();
            var message = JsonSerializer.Deserialize<Assets.Scripts.AgarioShared.Network.Message>(inputJson, options);
            
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
                    playerClient.PlayerState.ServerXPos = Math.Clamp(playerPositionMessage.X, -GameState.BoardSizeX/2, GameState.BoardSizeX/2);
                    playerClient.PlayerState.ServerYPos = Math.Clamp(playerPositionMessage.Y, -GameState.BoardSizeY/2, GameState.BoardSizeY/2);
                    // Console.WriteLine($"{playerClient.PlayerState.PlayerName} position: X={playerClient.PlayerState.ServerXPos},Y={playerClient.PlayerState.ServerYPos}");
                    break;

                case MessagesEnum.BoolMessage:
                    var boolmsg = JsonSerializer.Deserialize<BoolMessage>(inputJson, options);
                    Console.WriteLine(boolmsg.MessageName);
                    Console.WriteLine(boolmsg.BoolValue);
                    break;
                case MessagesEnum.VerifyValidOrbPickupMessage:
                    var verificationQuestionMessage =
                        JsonSerializer.Deserialize<VerifyValidOrbPickupMessage>(inputJson, options);
                    
                    // Console.WriteLine($"Server checking orb: #{verificationQuestionMessage.colOrbId} at position {verificationQuestionMessage.XCol},{verificationQuestionMessage.YCol}");
                    // Console.WriteLine($"Dictionary position: {GameState.orbData[verificationQuestionMessage.colOrbId].X},{GameState.orbData[verificationQuestionMessage.colOrbId].Y}");
                    
                     OrbValidation.ValidateOrbPickup(verificationQuestionMessage.colOrbId,
                        new Vector2(verificationQuestionMessage.XCol, verificationQuestionMessage.YCol), playerClient);
                    
                    break;
                case MessagesEnum.SpawnOrbMessage:
                    break;
    
                default:
                    throw new Exception("ERROR: Specific message not found on server!");
            }
           
        }
    }
}