using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using AgarioShared.Network;
using Assets.Scripts.AgarioShared.Model;
using Assets.Scripts.AgarioShared.Network;
using Assets.Scripts.AgarioShared.Network.Messages;
using Game;
using Unity.Mathematics;
using UnityEngine;

namespace Network
{
    public class MainClient : MonoBehaviour
    {
        public PlayerState playerState;
        public OrbController orbController;
        [SerializeField] private PlayerBroadcastPackageCollection playerBroadcastData;
        public int ServerID;
        public MessageHandler MessageHandler;
        private TcpClient playerTcpClient = new();
        public StreamWriter StreamWriter;
        public float UpdateLoopTime;
        public GameObject player;
        
        private void OnEnable()
        {
            MessageHandler.OnSpawnPlayer += SpawnPlayer;
        }

 
        
        private void Start()
        {
            PlayerSetup();
            StartCoroutine(UpdateLoop(UpdateLoopTime));

        }

        public void SpawnPlayer()
        {
            Debug.Log(playerState.CurrentXPos);
            Debug.Log(playerState.CurrentYPos);
            Instantiate(player, new Vector2(playerState.CurrentXPos, playerState.CurrentYPos), Quaternion.identity);
        }

        private async Task PlayerSetup()
        {
            await Init();
        }

        IEnumerator UpdateLoop(float updateLoopTime) // send to server
        {
            while (true)
            {
                //Update broadcasts
                playerBroadcastData.PlayerBroadCastPackage();
                yield return new WaitForSeconds(updateLoopTime);
            }
        }

        private async Task Init() // Stuff here move to playerstate or another new script? playerstate is kind of wrong since it is shared?
        {
            playerState = new PlayerState();
            var startGameData = FindObjectOfType<StartConnectionData>(); //TODO: the whole transfer data via object that does not destroy on scenechange feels ugly. Fix - maybe SO?
            playerState.PlayerName = startGameData.playerName;
            playerTcpClient = startGameData.TcpClient;
            StreamWriter = new StreamWriter(playerTcpClient.GetStream());
            new Task(() => MessageHandler.ReadMessage(playerTcpClient)).Start();
            await SendClientLogInMessage();
        }


        private async Task SendClientLogInMessage()
        {
            var logInMessage = new LogInMessage
            {
                MessageName = MessagesEnum.LogInMessage,
                PlayerName = playerState.PlayerName
            };
        
            await MessageHandler.SendMessageAsync(logInMessage, StreamWriter);
        }

        private void OnApplicationQuit()
        {
            playerTcpClient.Close();
        
        }
    }
}