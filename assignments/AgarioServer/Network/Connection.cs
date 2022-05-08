using System.Net.Sockets;
using System.Text.Json;

namespace AgarioServer;

public class Connection
{
    public readonly PlayerClient PlayerClient;
    public StreamWriter StreamWriter;
    private static int Id;

    public Connection()
    {
        PlayerClient= new PlayerClient()
        {
            PlayerServerId = ++Id,
            PlayerState = new PlayerState()
        };
    }
    public async Task Init(TcpClient tcpClient)
    {
        PlayerClient.PlayerTcpClient = tcpClient;
        StreamWriter = new StreamWriter(tcpClient.GetStream());
        StreamWriter.AutoFlush = true;
        new Task(()=>MessageHandler.ReadMessage(PlayerClient)).Start();
        
    }
}