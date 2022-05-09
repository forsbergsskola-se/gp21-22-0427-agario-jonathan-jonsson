using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;

public class MainClient : MonoBehaviour
{
    public PlayerState playerState;
    public OrbSpawner OrbSpawner;
    [SerializeField] private PlayerBroadcastPackageCollection playerBroadcastData;
    public int ServerID;
    public MessageHandler MessageHandler;
    private TcpClient playerTcpClient = new();
    public StreamWriter StreamWriter;
    public float UpdateLoopTime;

    private void Start()
    {
        SpawnPlayer();
        PlayerSetup();
        StartCoroutine(UpdateLoop(UpdateLoopTime));

    }

    private void SpawnPlayer()
    {
        //Implement new message getting start coords and instantiate player here
    }

    private async Task PlayerSetup()
    {
        await Init();
    }

 

    IEnumerator UpdateLoop(float updateLoopTime)
    {
        while (true)
        {
            //Update broadcasts
            playerBroadcastData.PlayerBroadCastPackage();
            yield return new WaitForSeconds(updateLoopTime);
        }
    }

    private async Task Init()
    {
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
}