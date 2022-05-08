namespace AgarioServer;

public class ServerDataPackages
{

    public static async Task SendServerDataPackages(Connection playerConnection)
    {
        await SendIllegalPositionNotification(playerConnection);
    }
    
    public static async Task SendIllegalPositionNotification(Connection playerConnection)
    {
        var illegalMovementMessage = new BoolMessage()
        {
            MessageName = MessagesEnum.BoolMessage,
            BoolValue = playerConnection.PlayerClient.PlayerState.IllegalMovement
        };

        var positionCorrection = new Vector2Message()
        {
            MessageName = MessagesEnum.Vector2Message,
            X = playerConnection.PlayerClient.PlayerState.XPos,
            Y = playerConnection.PlayerClient.PlayerState.YPos
        };
        
        await MessageHandler.SendMessageAsync(illegalMovementMessage, playerConnection.StreamWriter);
        await MessageHandler.SendMessageAsync(positionCorrection, playerConnection.StreamWriter);
    }
}