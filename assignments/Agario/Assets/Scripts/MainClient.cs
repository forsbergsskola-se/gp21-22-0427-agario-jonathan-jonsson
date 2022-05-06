using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainClient : MonoBehaviour
{
    [SerializeField] private Connection connection;

    [SerializeField] private TMP_InputField nameField;

    public void ConnectOnClick() => Connect();
    

    public async Task Connect()
    {
        var tcpClient = new TcpClient();
        await tcpClient.ConnectAsync(IPAddress.Loopback, 1313);
        var playerName = nameField.text;
        await connection.Init(tcpClient, playerName);

        await SendClientLogInMessage(connection);
        SceneManager.LoadSceneAsync("AgarioMain");
    }


    //update data to server here!


    private async Task SendClientLogInMessage(Connection connection)
    {
        var logInMessage = new LogInMessage
        {
            messageName = MessagesEnum.LogInMessage,
            playerName = connection.playerName
        };
        await MessageHandler.SendMessageAsync(logInMessage, connection.streamWriter);
    }
}