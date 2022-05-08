namespace AgarioServer;

public class MovementLegality
{
    public static bool EvaluateMovement(Vector2Message playerPositionMessage, PlayerClient playerClient)
    {
        if (!(playerPositionMessage.x > GameState.BoardSizeX / 2) &&
            !(playerPositionMessage.x < -GameState.BoardSizeX / 2) &&
            !(playerPositionMessage.y > GameState.BoardSizeY / 2) &&
            !(playerPositionMessage.y < -GameState.BoardSizeY / 2))
            return false;
        
        
        Console.WriteLine($"WARNING: {playerClient.PlayerState.PlayerName} is trying to exit the game board!");
        Console.WriteLine($"Corrected playerPosition to Server position, Vector2({playerClient.PlayerState.XPos},{playerClient.PlayerState.YPos}).");
        return true;

    }
}