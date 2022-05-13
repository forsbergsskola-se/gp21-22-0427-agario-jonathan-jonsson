using System.Numerics;
using AgarioServer.Network;
using Assets.Scripts.AgarioShared.Model;
using Assets.Scripts.AgarioShared.Network;
using Assets.Scripts.AgarioShared.Network.Messages;

namespace AgarioServer.Model;

public class OrbSpawner
{
    public static async Task SpawnOrb(PlayerClient playerClient)
    {
        Random random = new Random();
        var msg = new SpawnOrbMessage
        {
            MessageName = MessagesEnum.SpawnOrbMessage,
            
            X = random.Next(-GameState.BoardSizeX / 2, GameState.BoardSizeX / 2),
            Y = random.Next(-GameState.BoardSizeY / 2, GameState.BoardSizeY / 2)
        };
        Console.WriteLine(GameState.orbCoordinates.Contains(new Vector2(msg.X,msg.Y)));
        while (GameState.orbCoordinates.Contains(new Vector2(msg.X,msg.Y)))
        {
            msg.X = random.Next(-GameState.BoardSizeX / 2, GameState.BoardSizeX / 2);
            msg.Y = random.Next(-GameState.BoardSizeY / 2, GameState.BoardSizeY / 2);
        }
        
        Console.WriteLine($"Spawning orb at {msg.X},{msg.Y}");
        GameState.orbCoordinates.Add(new Vector2(msg.X,msg.Y));
        await MessageHandler.SendMessageAsync(msg, playerClient.StreamWriter);
    }

    public static async Task CheckAndSendValidationResponse(float X, float Y, PlayerClient playerClient)
    {
        var orbPosValidResponse = GameState.orbCoordinates.Contains(new Vector2(X, Y));
        var validMsg = new OrbPositionValidationResponseMessage()
        {
            MessageName = MessagesEnum.OrbPositionValidationResponseMessage,
            orbPositionValid = orbPosValidResponse,
            X = X,
            Y=Y
        };

        await MessageHandler.SendMessageAsync(validMsg, playerClient.StreamWriter);



    }
}