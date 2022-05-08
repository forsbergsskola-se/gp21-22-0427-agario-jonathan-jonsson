using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerClient : MonoBehaviour
{
    public PlayerState playerState;
    [SerializeField] private PlayerBroadcastPackageCollection playerBroadcastData;
    public int ServerID;
    public MessageHandler MessageHandler;
    private TcpClient playerTcpClient = new();
    public StreamWriter StreamWriter;
    public float UpdateLoopTime;

    private void Start()
    {
        PlayerSetup();
        StartCoroutine(UpdateLoop(UpdateLoopTime));

    }


    private async Task PlayerSetup()
    {
        await Init();
        await SetStartPosition();
    }

    private async Task SetStartPosition()
    {
        transform.position = new Vector3(playerState.XPos, playerState.YPos);
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