using System.Numerics;
using AgarioServer.Network;
using Assets.Scripts.AgarioShared.Model;
using Assets.Scripts.AgarioShared.Network;
using Assets.Scripts.AgarioShared.Network.Messages;

namespace AgarioServer.Model;

public class OrbSpawner
{
    public static int orbId;
    public static async Task SpawnOrb(PlayerClient playerClient)
    {
        Random random = new Random();
        var msg = new SpawnOrbMessage
        {
            MessageName = MessagesEnum.SpawnOrbMessage,
            orbId = orbId++,
            X = random.Next(-GameState.BoardSizeX / 2, GameState.BoardSizeX / 2),
            Y = random.Next(-GameState.BoardSizeY / 2, GameState.BoardSizeY / 2)
            
        };
        
        // Console.WriteLine(GameState.orbData.ContainsValue(new Vector2(msg.X,msg.Y)));
        
        while (GameState.orbData.ContainsValue(new Vector2(msg.X,msg.Y)))
        {
            msg.X = random.Next(-GameState.BoardSizeX / 2, GameState.BoardSizeX / 2);
            msg.Y = random.Next(-GameState.BoardSizeY / 2, GameState.BoardSizeY / 2);
        }
        
        Console.WriteLine($"Spawning orb at {msg.X},{msg.Y}");
        GameState.orbData.Add(msg.orbId,new Vector2(msg.X,msg.Y));
        await MessageHandler.SendMessageAsync(msg, playerClient.StreamWriter);
    }
}