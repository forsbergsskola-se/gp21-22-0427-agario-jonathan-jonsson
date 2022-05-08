namespace AgarioServer;

public class ServerDataPackages
{

    public static async Task SendServerDataPackages(Connection playerConnection)
    {
        SendIllegalPositionNotification(playerConnection);
    }
    
    public static async Task SendIllegalPositionNotification(Connection playerConnection)
    {
        var illegalMovementMessage = new BoolMessage()
        {
            messageName = MessagesEnum.BoolMessage,
            boolValue = playerConnection.PlayerClient.PlayerState.IllegalMovement
        };

        var positionCorrection = new Vector2Message()
        {
            messageName = MessagesEnum.Vector2Message,
            x = playerConnection.PlayerClient.PlayerState.XPos,
            y = playerConnection.PlayerClient.PlayerState.YPos
        };
        await MessageHandler.SendMessageAsync(illegalMovementMessage, playerConnection.StreamWriter);
        await MessageHandler.SendMessageAsync(positionCorrection, playerConnection.StreamWriter);
    }
}