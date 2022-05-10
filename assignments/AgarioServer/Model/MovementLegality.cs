using AgarioServer.Messages;
using AgarioServer.Network;

namespace AgarioServer.Model;

public class MovementLegality
{
    public static bool EvaluateMovement(Vector2Message playerPositionMessage, PlayerClient playerClient)
    {
        if (!(playerPositionMessage.X > GameState.BoardSizeX / 2) &&
            !(playerPositionMessage.X < -GameState.BoardSizeX / 2) &&
            !(playerPositionMessage.Y > GameState.BoardSizeY / 2) &&
            !(playerPositionMessage.Y < -GameState.BoardSizeY / 2))
            return false;
        
        
        Console.WriteLine($"WARNING: {playerClient.PlayerState.PlayerName} is trying to exit the game board!");
        Console.WriteLine($"Corrected playerPosition to Server position, Vector2({playerClient.PlayerState.XPos},{playerClient.PlayerState.YPos}).");
        return true;

    }
}