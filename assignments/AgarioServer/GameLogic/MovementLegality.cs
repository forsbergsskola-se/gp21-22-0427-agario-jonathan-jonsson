namespace AgarioServer;

public class MovementLegality
{
    public static bool EvaluateMovement(Vector2Message playerPositionMessage, PlayerClient playerClient)
    {
        if (playerPositionMessage.x > GameState.boardSizeX / 2 ||
            playerPositionMessage.x < -GameState.boardSizeX / 2 ||
            playerPositionMessage.y > GameState.boardSizeY / 2 ||
            playerPositionMessage.y < -GameState.boardSizeY / 2)
        {
            Console.WriteLine($"WARNING: {playerClient.playerState.playerName} is trying to exit the game board!");
            Console.WriteLine($"Corrected playerPosition to Server position, Vector2({playerClient.playerState.xPos},{playerClient.playerState.yPos}).");
            return true;
        }

        return false;

    }
}