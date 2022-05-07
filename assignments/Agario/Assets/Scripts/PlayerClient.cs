using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerClient : MonoBehaviour
{
    private TcpClient playerTcpClient = new TcpClient();
   [SerializeField] public PlayerState playerState;
    public int ServerID;
    public MessageHandler MessageHandler;
    public StreamWriter streamWriter;
    
    
    private async Task PlayerSetup()
    {
       await Init();
       SetStartPosition();

    }

    private void Start()
    {
        PlayerSetup();
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
        new Task(()=>MessageHandler.ReadMessage(playerTcpClient)).Start();
        await SendClientLogInMessage();
    }

 
    private async Task SendClientLogInMessage()
    {
        var logInMessage = new LogInMessage
        {
            messageName = MessagesEnum.LogInMessage,
            playerName =  playerState.playerName
        };
        await MessageHandler.SendMessageAsync(logInMessage, streamWriter);
    }

   
}