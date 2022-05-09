using System.Threading.Tasks;
using Messages;
using UnityEngine;

public class PlayerBroadcastPackageCollection : MonoBehaviour
{
    [SerializeField] private MainClient mainClient;


    public async Task PlayerBroadCastPackage()
    {
        await UpdatePosToServer();
    }


    private async Task UpdatePosToServer()
    {
        var position = mainClient.playerState.transform.position;
        var msg = new Vector2Message
        {
            MessageName = MessagesEnum.Vector2Message,
            X = position.x,
            Y = position.y
        };

        await mainClient.MessageHandler.SendMessageAsync(msg, mainClient.StreamWriter);
    }
}