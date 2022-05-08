using System.Threading.Tasks;
using Messages;
using UnityEngine;

public class PlayerBroadcastPackageCollection : MonoBehaviour
{
    [SerializeField] private PlayerClient playerClient;


    public async Task PlayerBroadCastPackage()
    {
        await UpdatePosToServer();
    }


    private async Task UpdatePosToServer()
    {
        var position = transform.position;
        var msg = new Vector2Message
        {
            MessageName = MessagesEnum.Vector2Message,
            X = position.x,
            Y = position.y
        };

        await playerClient.MessageHandler.SendMessageAsync(msg, playerClient.StreamWriter);
    }
}