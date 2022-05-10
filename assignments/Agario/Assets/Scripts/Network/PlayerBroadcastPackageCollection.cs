using System.Threading.Tasks;
using AgarioShared.Network;
using Assets.Scripts.AgarioShared.Network;
using Assets.Scripts.AgarioShared.Network.Messages;
using UnityEngine;

namespace Network
{
    public class PlayerBroadcastPackageCollection : MonoBehaviour
    {
        [SerializeField] private MainClient mainClient;


        public async Task PlayerBroadCastPackage()
        {
            await UpdatePosToServer();
        }


        private async Task UpdatePosToServer()
        {
            var currentPlayerPosition = mainClient.playerState.GetPlayerCurrentPosition();
            var msg = new Vector2Message
            {
                MessageName = MessagesEnum.Vector2Message,
                X = currentPlayerPosition.X,
                Y = currentPlayerPosition.Y
            };
            Debug.Log(msg.X + " " +msg.Y);
            await MessageHandler.SendMessageAsync(msg, mainClient.StreamWriter);
        }
    }
}