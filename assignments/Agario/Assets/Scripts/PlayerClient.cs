using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerClient : MonoBehaviour
{
    private TcpClient playerTcpClient = new TcpClient();
   [SerializeField] private PlayerState playerState;
   public Connection connection;
    public int ServerID;
    
    
    private void Start()
    {
        Init();
    }

    public async Task Init()
    {
        connection = new Connection();
        var starGameData = FindObjectOfType<StartConnectionData>();
        playerState.playerName = starGameData.playerName;
        playerTcpClient = starGameData.TcpClient;
        
        await connection.Init(playerTcpClient,playerState.playerName);

        await SendClientLogInMessage(connection);
    }


    //update data to server here!


    private async Task SendClientLogInMessage(Connection connections)
    {
        var logInMessage = new LogInMessage
        {
            messageName = MessagesEnum.LogInMessage,
            playerName = connections.playerName
        };
        await MessageHandler.SendMessageAsync(logInMessage, connections.streamWriter);
    }
}