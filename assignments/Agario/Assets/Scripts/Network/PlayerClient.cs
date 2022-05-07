using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerClient : MonoBehaviour
{
    [SerializeField] public PlayerState playerState;
    public int ServerID;
    public MessageHandler MessageHandler;
    private TcpClient playerTcpClient = new();
    public StreamWriter streamWriter;

    private void Start()
    {
        PlayerSetup();
    }


    private async Task PlayerSetup()
    {
        await Init();
        SetStartPosition();
    }

    private void SetStartPosition()
    {
        transform.position = new Vector3(playerState.XPos, playerState.YPos);
    }

    public async Task Init()
    {
        var starGameData = FindObjectOfType<StartConnectionData>();
        playerState.playerName = starGameData.playerName;
        playerTcpClient = starGameData.TcpClient;
        streamWriter = new StreamWriter(playerTcpClient.GetStream());
        new Task(() => MessageHandler.ReadMessage(playerTcpClient)).Start();
        await SendClientLogInMessage();
    }


    private async Task SendClientLogInMessage()
    {
        var logInMessage = new LogInMessage
        {
            messageName = MessagesEnum.LogInMessage,
            playerName = playerState.playerName
        };
        await MessageHandler.SendMessageAsync(logInMessage, streamWriter);
    }
}