using Assets.Scripts.AgarioShared.Network;
using Assets.Scripts.AgarioShared.Network.Messages;

namespace AgarioServer.Network;

public class ServerDataPackages
{

    public static async Task SendServerDataPackages(PlayerClient playerClient)
    {
        
        await SendIllegalPositionNotification(playerClient);
    }

    private static async Task SendIllegalPositionNotification(PlayerClient playerClient)
    {
        var illegalMovementMessage = new BoolMessage()
        {
            MessageName = MessagesEnum.BoolMessage,
            BoolValue = playerClient.PlayerState.IllegalMovement
        };

        var positionCorrection = new Vector2Message()
        {
            MessageName = MessagesEnum.Vector2Message,
            X = playerClient.PlayerState.ServerXPos,
            Y = playerClient.PlayerState.ServerYPos
        };

        await MessageHandler.SendMessageAsync(illegalMovementMessage, playerClient.StreamWriter);
        await MessageHandler.SendMessageAsync(positionCorrection, playerClient.StreamWriter);
    }
    
}