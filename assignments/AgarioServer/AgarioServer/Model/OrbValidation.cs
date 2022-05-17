using System.Numerics;
using AgarioServer.Network;
using Assets.Scripts.AgarioShared.Model;
using Assets.Scripts.AgarioShared.Network;
using Assets.Scripts.AgarioShared.Network.Messages;

namespace AgarioServer.Model;

public static class OrbValidation
{
    private static float distTolerance = 0.1f;
    public static async Task ValidateOrbPickup(int orbId, Vector2 playerPosition, PlayerClient playerClient)
    {
        var orbPos = new Vector2(GameState.orbData[orbId].X, GameState.orbData[orbId].Y); // server orb position
        var dVector = playerPosition - orbPos; // vector between player and orb
        var dVectorMag = MathF.Sqrt(MathF.Pow(dVector.X, 2) + MathF.Pow(dVector.Y, 2)); // magnitude = SQRT(x^2+y^2)

       var  validationBool = MathF.Abs(dVectorMag - playerClient.PlayerState.Size/2f - 1/2f) < distTolerance; // Mag - playerradius - orbradius = approx 0 ???

       
       //Send message here to client with result of check
       var msg = new OrbValidationResponseMessage()
       {
           MessageName = MessagesEnum.OrbValidationResponseMessage,
           orbValid = validationBool,
           orbId = orbId
       };

       await MessageHandler.SendMessageAsync(msg, playerClient.StreamWriter);
       
       if (msg.orbValid)
       {
           //Send score update here
       }
    }
}