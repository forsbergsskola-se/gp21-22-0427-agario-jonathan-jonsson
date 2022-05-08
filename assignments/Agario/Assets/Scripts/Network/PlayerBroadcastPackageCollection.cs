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


    public async Task UpdatePosToServer()
    {
        var msg = new Vector2Message
        {
            messageName = MessagesEnum.Vector2Message,
            x = transform.position.x,
            y = transform.position.y
        };

        playerClient.MessageHandler.SendMessageAsync(msg, playerClient.streamWriter);
    }
}