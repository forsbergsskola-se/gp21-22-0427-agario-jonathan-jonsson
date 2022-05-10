using System.Net.Sockets;
using AgarioServer.Model;

namespace AgarioServer.Network;

public class PlayerClient
{
    public PlayerState PlayerState;
    public int PlayerServerId;
    public TcpClient PlayerTcpClient;
    public StreamWriter StreamWriter;

    public PlayerClient(TcpClient tcpClient, int id)
    {
        PlayerServerId = id;
        PlayerTcpClient = tcpClient;
        PlayerState = new PlayerState();
        StreamWriter = new StreamWriter(PlayerTcpClient.GetStream())
        {
            AutoFlush = true
        };
        
        new Task(() => MessageHandler.ReadMessage(this)).Start();
        
    }
}