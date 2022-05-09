namespace AgarioServer;

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
        Console.WriteLine($"Spawning orb at {msg.X},{msg.Y}");

        await MessageHandler.SendMessageAsync(msg, playerClient.StreamWriter);
    } 
}